#!/bin/bash
# Copyright (C) 2022 Andrew Rioux
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

cd $(git rev-parse --show-toplevel)

function setup_minikube() {
    minikube addons enable ingress
}

function base_init() {
    kubectl apply -f k8s/base.yaml
}

function setup_mysql() {
    kubectl apply -f k8s/mysql-dev.yaml
}

RUN_SETUP_MINIKUBE=0
RUN_BASE_INIT=0
RUN_SETUP_MYSQL=0

while getopts "miMbah" opt; do
    case $opt in
        m)
            RUN_SETUP_MINIKUBE=1
            ;;
        i)
            RUN_BASE_INIT=1
            ;;
        M)
            RUN_SETUP_MYSQL=1
            ;;
        a)
            RUN_SETUP_MINIKUBE=1
            RUN_BASE_INIT=1
            RUN_SETUP_MYSQL=1
            RUN_INITIAL_BUILD_AND_DEPLOY=1
            ;;
        h)
            cat - <<EOD
Performs initial setup for developers in their environments,
performing actions such as configuring minikube, initializing
EOD
            exit 0
            ;;
    esac
done

[ "$RUN_SETUP_MINIKUBE" = "1" ] && setup_minikube
[ "$RUN_BASE_INIT" = "1" ] && base_init
[ "$RUN_SETUP_MYSQL" = "1" ] && setup_mysql