#!/bin/sh
# gha-build-webmaster-dashboard-env.sh: builds the integration test environment
# for the webmaster dashboard for GitHub Actions
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

dotnet build

export PROXY_NAME=*.localunitplanner.org
export PROXY_PORT=3000
export API_SERVER_URL=http://localhost:5000
export ELM_REACTOR_URL=http://localhost:8000
export PROJECT_STATIC_DIR=$PWD/Client/static
export INDEX_FILE=/webmasterdashboard.html
export E2E_TEST_SEED_SERVICE=http://localhost:4090

envsubst '$PROXY_NAME,$PROXY_PORT,$API_SERVER_URL,$ELM_REACTOR_URL,$PROJECT_STATIC_DIR,$INDEX_FILE,$E2E_TEST_SEED_SERVICE' < $PWD/Client/nginx/github-actions-regular.conf.template > $PWD/Client/nginx/github-actions.conf

mkdir Client/out
cd Client
elm make --optimize --output=static/out/WebmasterDashboard.js src/WebmasterDashboard.elm

cd $(git rev-parse --show-toplevel)