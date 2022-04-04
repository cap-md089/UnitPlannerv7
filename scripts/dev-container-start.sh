#!/bin/sh
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

cd /workspaces/UnitPlannerv7

mkdir -p ssh
rm ssh/x11-keys
minikube start
kubectl config set-context --current --namespace=unitplannerv7
ssh-keygen -N "" -f ssh/x11-keys -b 1024
mv ssh/x11-keys.pub ~/.ssh/authorized_keys
ssh-add ssh/x11-keys
sudo service ssh start
nohup bash -c 'minikube mount /workspaces/UnitPlannerv7:/workspaces/UnitPlannerv7 &' > ~/minikube-mount.log 2>&1
kubectl wait -n ingress-nginx deployment --for condition=Available=True -l app.kubernetes.io/name=ingress-nginx
nohup bash -c 'kubectl port-forward -n ingress-nginx service/ingress-nginx-controller 80:80 &' > ~/port-forwarding.log 2>&1

sleep infinity