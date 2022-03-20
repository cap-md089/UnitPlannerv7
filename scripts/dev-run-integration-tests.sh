# Copyright (C) 2022 Andrew Rioux
# 
# This file is part of UnitPlannerv7.
# 
# UnitPlannerv7 is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 2 of the License, or
# (at your option) any later version.
# 
# UnitPlannerv7 is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with UnitPlannerv7.  If not, see <http://www.gnu.org/licenses/>.

set -eux

cd $(git rev-parse --show-toplevel)

export DB=db_integration_tests

docker-compose run -f docker-compose.yml -f docker-compose.test.yml cypress_tests_client
docker-compose run -f docker-compose.yml -f docker-compose.test.yml cypress_tests_webmaster_dashboard
docker-compose run -f docker-compose.yml -f docker-compose.test.yml cypress_tests_reactor