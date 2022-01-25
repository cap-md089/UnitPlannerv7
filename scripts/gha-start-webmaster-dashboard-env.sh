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

sudo systemctl start mysql.service

export ConnectionStrings__MainDB="server=localhost;uid=root;password=root;database=test"
export RUNNING_IN_CI=1

dotnet run --project Apis/Main.IntegrationTests/UnitPlanner.Apis.Main.IntegrationTests.csproj > /dev/null &
dotnet run --project Apis/Main/UnitPlanner.Apis.Main.csproj > /dev/null &

sudo nginx -c $PWD/Client/nginx/github-actions.conf > /dev/null