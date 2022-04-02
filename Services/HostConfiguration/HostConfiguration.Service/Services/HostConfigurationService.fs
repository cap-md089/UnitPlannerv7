// Copyright (C) 2022 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

module UnitPlanner.Services.HostConfiguration.Service.Services

open System
open System.Threading.Tasks

open Grpc.Core
open Microsoft.Extensions.Logging
open k8s
open k8s.Models

open UnitPlanner.Services.HostConfiguration.Protos

type HostConfigurationService(logger : ILogger<HostConfigurationService>) =
    inherit UnitPlanner.Services.HostConfiguration.Protos.HostConfiguration.HostConfigurationBase()

    let _logger : ILogger<HostConfigurationService> = logger

    override this.SetHosts(request : HostChangeRequest, context : ServerCallContext) : Task<HostChangeResponse> =
        let tlsClusterIssuerEnv = Environment.GetEnvironmentVariable("CLUSTER_ISSUER")
        let tlsClusterIssuer =
            if tlsClusterIssuerEnv = null
                then None
                else Some tlsClusterIssuerEnv

        let targetNamespace = "unitplannerv7"
        let ingressName = $"account-ingress-{request.AccountID}"

        _logger.LogInformation($"Setting hosts for {request.AccountID}")

        async {
            let cluster = new Kubernetes(KubernetesClientConfiguration.InClusterConfig())

            let! currentIngressRule = async {
                try
                    let! rule = cluster.ReadNamespacedIngressAsync(ingressName, targetNamespace)
                                    |> Async.AwaitTask
                    return Some rule
                with
                | ex -> return None
            }

            let newIngressRule = new V1Ingress(
                apiVersion = "networking.k8s.io/v1",
                kind = "Ingress",
                metadata = new V1ObjectMeta (
                    name = ingressName
                ),
                spec = new V1IngressSpec(
                    ingressClassName = "nginx",
                    rules = (seq {
                        for host in request.Hosts do
                            yield new V1IngressRule(
                                host = host,
                                http = new V1HTTPIngressRuleValue(
                                    paths = [|
                                        new V1HTTPIngressPath(
                                            path = "/api",
                                            pathType = "Prefix",
                                            backend = new V1IngressBackend(
                                                service = new V1IngressServiceBackend(
                                                    name = "base-api",
                                                    port = new V1ServiceBackendPort(
                                                        number = 5000
                                                    )
                                                )
                                            )
                                        );
                                        new V1HTTPIngressPath(
                                            path = "/",
                                            pathType = "Prefix",
                                            backend = new V1IngressBackend(
                                                service = new V1IngressServiceBackend(
                                                    name = "client-main-static",
                                                    port = new V1ServiceBackendPort(
                                                        number = 80
                                                    )
                                                )
                                            )
                                        )
                                    |]
                                )
                            )
                    }
                        |> Seq.toArray)
                )
            )

            match tlsClusterIssuer with
            | Some issuer -> newIngressRule.Metadata.Annotations.Add("cluster-issuer.io/ingress", issuer)
                             newIngressRule.Spec.Tls <- [|
                                new V1IngressTLS(
                                    secretName = $"tls-secret-ingress-{request.AccountID}",
                                    hosts = request.Hosts
                                )
                             |]
            | None -> ()

            match currentIngressRule with
            | None ->
                do! cluster.CreateNamespacedIngressAsync(newIngressRule, targetNamespace)
                    |> Async.AwaitTask
                    |> Async.Ignore
            | Some _ ->
                do! cluster.ReplaceNamespacedIngressAsync(newIngressRule, ingressName, targetNamespace)
                    |> Async.AwaitTask
                    |> Async.Ignore

            return new HostChangeResponse (ErrorMessage = "", HasError = false)
        }
            |> Async.StartAsTask
    
    override this.RemoveHost(request : HostRemoveRequest, context : ServerCallContext) : Task<HostChangeResponse> =
        let targetNamespace = "unitplannerv7"
        let ingressName = $"account-ingress-{request.AccountID}"

        _logger.LogInformation($"Removing ingress for {request.AccountID}")

        async {
            let cluster = new Kubernetes(KubernetesClientConfiguration.InClusterConfig())

            do! cluster.DeleteNamespacedIngressAsync(ingressName, targetNamespace)
                |> Async.AwaitTask
                |> Async.Ignore

            return new HostChangeResponse (ErrorMessage = "", HasError = false)
        }
            |> Async.StartAsTask