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

cd $(git rev-parse --show-toplevel)

export DISPLAY=:0

if ! test -S /tmp/.X11-unix/X0; then
    export DISPLAY=docker.for.win.localhost:0
fi

echo "Using display: $DISPLAY"
node_modules/.bin/cypress open --project .