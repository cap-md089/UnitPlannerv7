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

$IP = (Get-NetIPAddress -AddressFamily IPv4 | Where-Object { $_.InterfaceAlias -like 'Wi-Fi' })[0].IPAddress

echo "Using display ${IP}:0"

docker-compose run -e DISPLAY=${IP}:0 cypress_test_runner_webmaster_dashboard_linux