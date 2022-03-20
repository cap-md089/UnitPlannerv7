#!/bin/bash
# Copyright (C) 2022 Andrew Rioux
# 
# Builds a specified container, or all of them. Doesn't build any at first unless a
# class is specified, either client or server containers (-C or -S respectively)
#
# Examples:
#     Build all containers:
#         scripts/dev-build-containers.sh -C -S
#
#     Build all client containers:
#         scripts/dev-build-containers.sh -C
#
#     Build the main API service:
#         scripts/dev-build-containers.sh -s Apis/Main
# 
# Options:
#     -r: Builds the containers in release mode, as opposed to debug mode
# 
#     -s [SERVICE]: Builds the specified service. If not specified, all services
#         are redeployed. Service can be one of:
#             Apis/Main
#             Services/Authentication
#             Services/Capwatch
#             Services/Files
#             Services/Graph
#             Services/HostConfiguration
# 
#         This option can be specified multiple times.
#
#     -c [CLIENT]: Builds the specified client. If not specified, all
#         clients are built. CLIENT can be one of:
#             main
#             admin
#
#         This option can be specified multiple times
#
#     -S: Specifies to build all server services
#
#     -C: Specifies to build all client services
# 
#     -h: Prints this help message, and exits.
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

set -eu

pushd $(git rev-parse --show-toplevel) > /dev/null

BUILD_CONFIGURATION=DEBUG
TARGET_SERVICES=()
TARGET_CLIENTS=()

eval $(minikube docker-env)

while getopts "SChrs:c:" opt; do
    case $opt in
        r)
            BUILD_CONFIGURATION=RELEASE
            ;;

        s)
            TARGET_SERVICES+=($OPTARG)
            ;;

        S)
            TARGET_SERVICES=("Apis/Main" "Services/Authentication" "Services/Capwatch" "Services/Files" "Services/Graph" "Services/HostConfiguration")
            ;;

        c)
            TARGET_CLIENTS+=($OPTARG)
            ;;

        C)
            TARGET_CLIENTS=("main" "admin")
            ;;

        h)
            cat - <<EOD
Builds a specified container, or all of them. Doesn't build any at first unless a
class is specified, either client or server containers (-C or -S respectively)

Examples:
    Build all containers:
        scripts/dev-build-containers.sh -C -S

    Build all client containers:
        scripts/dev-build-containers.sh -C

    Build the main API service:
        scripts/dev-build-containers.sh -s Apis/Main

Options:
    -r: Builds the containers in release mode, as opposed to debug mode

    -s [SERVICE]: Builds the specified service. If not specified, all services
        are built. SERVICE can be one of:
            Apis/Main
            Services/Authentication
            Services/Capwatch
            Services/Files
            Services/Graph
            Services/HostConfiguration

        This option can be specified multiple times.

    -c [CLIENT]: Builds the specified client. If not specified, all
        clients are built. CLIENT can be one of:
            main
            admin

        This option can be specified multiple times

    -S: Specifies to build all server services

    -C: Specifies to build all client services

    -h: Prints this help message, and exits.
EOD
            exit 0
            ;;
    esac
done

for service in "${TARGET_SERVICES[@]}"; do
    if [ "$BUILD_CONFIGURATION" = "DEBUG" ]; then
        docker build --tag=$(echo ghcr.io/cap-md089/UnitPlannerv7/$service | sed 's/[A-Z]/\L&/g') --file=$service/Dockerfile --build-arg RELEASE_CONFIGURATION=Debug --target=dev .
    else
        docker build --tag=$(echo ghcr.io/cap-md089/UnitPlannerv7/$service | sed 's/[A-Z]/\L&/g') --file=$service/Dockerfile --build-arg RELEASE_CONFIGURATION=Release --target=final .
    fi
done

for client in "${TARGET_CLIENTS[@]}"; do
    if [ "$BUILD_CONFIGURATION" = "DEBUG" ]; then
        docker build --tag=ghcr.io/cap-md089/unitplannerv7/client/$client --file=Client/Dockerfile --build-arg 'RUNTIME_ARG=--debug' --target=final-$client .
    else
        docker build --tag=ghcr.io/cap-md089/unitplannerv7/client/$client --file=Client/Dockerfile --build-arg 'RUNTIME_ARG=--optimize' --target=final-$client .
    fi
done

popd > /dev/null