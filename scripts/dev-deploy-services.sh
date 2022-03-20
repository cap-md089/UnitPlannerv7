#!/bin/bash
# Copyright (C) 2022 Andrew Rioux
#
# Deploys all the services. The following environment variables can be used
# to control the operation of this program:
#
# ASPNETCORE_ENVIRONMENT: Either 'Development' or 'Release'
#
# BASE_API_TAG: Which release tag to use when pulling the image
# SERVICE_USE_PRODUCTION_AUTH_VALUE: '0' or '1', regarding whether or not to use the gRPC service or simply use a stub (in development only)
# SERVICE_USE_PRODUCTION_CAPWATCH_VALUE
# SERVICE_USE_PRODUCTION_FILES_VALUE
# SERVICE_USE_PRODUCTION_GRAPH_VALUE
# SERVICE_USE_PRODUCTION_HOSTCONFIGURATION_VALUE
#
# AUTHENTICATION_TAG: Which release tag to use when pulling the image
# 
# CAPWATCH_TAG: Which release tag to use when pulling the image
# 
# FILES_TAG: Which release tag to use when pulling the image
# 
# GRAPH_TAG: Which release tag to use when pulling the image
# 
# HOST_CONFIGURATION_TAG: Which release tag to use when pulling the image
#
# CLIENT_ADMIN_TAG: Which release tag to use when pulling the image
#
# CLIENT_MAIN_TAG: Which release tag to use when pulling the image
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
Deploys all the services. The following environment variables can be used
to control the operation of this program:

ASPNETCORE_ENVIRONMENT: Either 'Development' or 'Release'

BASE_API_TAG: Which release tag to use when pulling the image
SERVICE_USE_PRODUCTION_AUTH_VALUE: '0' or '1', regarding whether or not to use the gRPC service or simply use a stub (in development only)
SERVICE_USE_PRODUCTION_CAPWATCH_VALUE
SERVICE_USE_PRODUCTION_FILES_VALUE
SERVICE_USE_PRODUCTION_GRAPH_VALUE
SERVICE_USE_PRODUCTION_HOSTCONFIGURATION_VALUE

AUTHENTICATION_TAG: Which release tag to use when pulling the image

CAPWATCH_TAG: Which release tag to use when pulling the image

FILES_TAG: Which release tag to use when pulling the image

GRAPH_TAG: Which release tag to use when pulling the image

HOST_CONFIGURATION_TAG: Which release tag to use when pulling the image

CLIENT_ADMIN_TAG: Which release tag to use when pulling the image

CLIENT_MAIN_TAG: Which release tag to use when pulling the image
EOD
            exit 0
            ;;
    esac
done

if [ "${#TARGET_SERVICES[@]}" -eq 0 ]; then
    TARGET_SERVICES=("base-api" "service-authentication" "service-capwatch" "service-files" "service-graph" "service-host-configuration" "client-admin-dev" "client-main-dev")
fi

export ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT:-Development}

export BASE_API_TAG=${BASE_API_TAG:-latest}
export SERVICE_USE_PRODUCTION_AUTH_VALUE=${SERVICE_USE_PRODUCTION_AUTH_VALUE:-0}
export SERVICE_USE_PRODUCTION_CAPWATCH_VALUE=${SERVICE_USE_PRODUCTION_CAPWATCH_VALUE:-0}
export SERVICE_USE_PRODUCTION_FILES_VALUE=${SERVICE_USE_PRODUCTION_FILES_VALUE:-0}
export SERVICE_USE_PRODUCTION_GRAPH_VALUE=${SERVICE_USE_PRODUCTION_GRAPH_VALUE:-0}
export SERVICE_USE_PRODUCTION_HOSTCONFIGURATION_VALUE=${SERVICE_USE_PRODUCTION_HOSTCONFIGURATION_VALUE:-0}

export AUTHENTICATION_TAG=${AUTHENTICATION_TAG:-latest}

export CAPWATCH_TAG=${AUTHENTICATION_TAG:-latest}

export FILES_TAG=${AUTHENTICATION_TAG:-latest}

export GRAPH_TAG=${AUTHENTICATION_TAG:-latest}

export HOST_CONFIGURATION_TAG=${HOST_CONFIGURATION_TAG:-latest}

export CLIENT_ADMIN_TAG=${CLIENT_ADMIN_TAG:-latest}

export CLIENT_MAIN_TAG=${CLIENT_MAIN_TAG:-latest}

for conf in "${TARGET_SERVICES[@]}"; do
    cat k8s/$conf.yaml | envsubst | kubectl apply -f -
done

popd > /dev/null