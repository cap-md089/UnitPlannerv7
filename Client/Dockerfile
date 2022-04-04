# Dockerfile: Used to test and run Elm reactor, as well as properly proxy static files and API request
#
# Copyright (C) 2021 Andrew Rioux
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


FROM node:16-alpine AS base

RUN apk add curl jq && \
    curl -L -o elm.gz https://github.com/elm/compiler/releases/download/0.19.1/binary-for-linux-64-bit.gz && \
    gunzip elm.gz && \
    chmod +x elm && \
    mv elm /usr/bin && \
    apk del curl && \
    npm i -g elm-test elm-format elm-live

ENTRYPOINT [ "/usr/local/bin/elm-live" ]

FROM base AS reactor

WORKDIR /workspaces/UnitPlannerv7/Client

RUN apk add coreutils

ENTRYPOINT [ "/usr/bin/elm" ]
CMD [ "reactor", "--port=8000" ]

FROM base AS build-admin

WORKDIR /app

COPY Client /app

ARG RUNTIME_ARG=--debug

RUN elm make $RUNTIME_ARG --output=/app/WebmasterDashboard.js /app/src/WebmasterDashboard.elm

FROM base AS build-client

WORKDIR /app

COPY Client /app

ARG RUNTIME_ARG=--debug

RUN elm make $RUNTIME_ARG --output=/app/Main.js /app/src/Main.elm

FROM nginx AS final-admin

COPY Client/static /var/www/html/static
COPY Client/nginx/production.conf /etc/nginx/conf.d/default.conf

COPY --from=build-admin /app/WebmasterDashboard.js /var/www/html/out/WebmasterDashboard.js

COPY Client/static/webmasterdashboard.html /var/www/html/index.html

CMD nginx -g 'daemon off;'

FROM nginx AS final-main

COPY Client/static /var/www/html/static
COPY Client/nginx/production.conf /etc/nginx/conf.d/default.conf

COPY --from=build-client /app/Main.js /var/www/html/out/Main.js

COPY Client/static/main.html /var/www/html/index.html

CMD nginx -g 'daemon off;'