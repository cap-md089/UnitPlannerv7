# Dockerfile: Used to provide common tools and installation targets
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

FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN useradd dev && \
    mkdir -p /home/dev && \
    chown -R dev /home/dev
USER dev

RUN dotnet tool install --global dotnet-ef

ENV PATH=$PATH:/home/dev/.dotnet/tools

CMD [ "/bin/bash" ]