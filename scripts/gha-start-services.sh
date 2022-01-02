#!/bin/sh
# gha-start-services.sh: used to start all programs in GitHub Actions for integration tests
#
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

set -eux

cd $(git rev-parse --show-toplevel)

export PROXY_NAME=*.localunitplanner.org
export PROXY_PORT=3000
export API_SERVER_URL=http://localhost:5000
export ELM_REACTOR_URL=http://localhost:8000
export PROJECT_STATIC_DIR=$PWD/Client/static

envsubst '$PROXY_NAME,$PROXY_PORT,$API_SERVER_URL,$ELM_REACTOR_URL,$PROJECT_STATIC_DIR' < $PWD/Client/nginx/github-actions.conf.template > $PWD/Client/nginx/github-actions.conf

dotnet run --project Services/Development/Authentication/UnitPlanner.Services.Authentication.Development.csproj > /dev/null &
dotnet run --project Services/Development/Capwatch/UnitPlanner.Services.Capwatch.Development.csproj > /dev/null &
dotnet run --project Services/Development/Files/UnitPlanner.Services.Files.Development.csproj > /dev/null &
dotnet run --project Services/Development/Graph/UnitPlanner.Services.Graph.Development.csproj > /dev/null &
dotnet run --project Apis/Main/UnitPlanner.Apis.Main.csproj > /dev/null &

cd Client
elm reactor --port=8000 > /dev/null &
cd $(git rev-parse --show-toplevel)

sudo nginx -c $PWD/Client/nginx/github-actions.conf > /dev/null