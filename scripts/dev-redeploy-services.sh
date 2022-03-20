#!/bin/bash
# Copyright (C) 2022 Andrew Rioux
#
# Redeploys specified services, or all services if none are specified.
# Options:
#     -s [SERVICE-NAME]: Redeploys the specified service. By default redeploys all services.
#         Can be one of:
#             base-api
#             host-configuration
# 
#         This option can be specified multiple times
# 
#     -h: Prints a help message, and exits
# 
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU Affero General Public License as
# published by the Free Software Foundation, either version 3 of the
# License, or (at your option) any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU Affero General Public License for more details.
# 
# You should have received a copy of the GNU Affero General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.

pushd $(git rev-parse --show-toplevel) > /dev/null

TARGET_SERVICES=()

while getopts "hs:" opt; do
    case $opt in
        s)
            TARGET_SERVICES+=($OPTARG)
            ;;

        h)
            cat - <<EOD
Redeploys specified services, or all services if none are specified.
Options:
    -s [SERVICE-NAME]: Redeploys the specified service. If not specified,
        all services are redeployed. Can be one of:
            base-api
            client-admin-dev
            client-main-dev
            service-authentication
            service-capwatch
            service-files
            service-graph
            service-host-configuration

        This option can be specified multiple times

    -h: Prints this help message, and exits
EOD
            exit 0
            ;;
    esac
done

if [ "${#TARGET_SERVICES[@]}" -eq 0 ]; then
    TARGET_SERVICES=("base-api" "client-admin-dev" "client-main-dev" "service-authentication" "service-capwatch" "service-files" "service-graph" "service-host-configuration")
fi

for service in "${TARGET_SERVICES[@]}"; do
    kubectl delete -f k8s/$service.yaml
done

scripts/dev-deploy-services.sh "$@"

popd > /dev/null