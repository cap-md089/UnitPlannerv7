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

pushd $(git rev-parse --show-toplevel) > /dev/null

while getopts "h" opt; do
    case $opt in
        h)
            cat - <<EOD
Interactive program to initialize the current Kubernetes configuration in a more general fashion
EOD
            exit 0
            ;;
    esac
done

function setup_mysql_password() {
    read -p "Enter MySQL user: " mysql_user
    read -s -p "Enter MySQL password: " mysql_password
    echo

    kubectl apply -f- <<EOD
apiVersion: v1
kind: Secret
metadata:
  namespace: unitplannerv7
  name: mysql-password
type: kubernetes.io/basic-auth
stringData:
  connectionString: server=db;uid=${mysql_user};pwd=${mysql_password}
  username: ${mysql_user}
  password: ${mysql_password}
EOD
}

kubectl apply -f k8s/base.yaml
setup_mysql_password

popd > /dev/null
