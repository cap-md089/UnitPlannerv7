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

RUN apk add curl && \
    curl -L -o elm.gz https://github.com/elm/compiler/releases/download/0.19.1/binary-for-linux-64-bit.gz && \
    gunzip elm.gz && \
    chmod +x elm && \
    mv elm /usr/bin && \
    apk del curl && \
    npm i -g elm-test elm-format

ENTRYPOINT [ "/usr/bin/elm" ]

FROM nginx AS development-proxy

CMD bash -c "envsubst '\$PROXY_NAME,\$PROXY_PORT,\$API_SERVER_URL,\$ELM_REACTOR_URL' < /etc/nginx/extra-conf/development.conf.template > /etc/nginx/conf.d/default.conf && nginx -g 'daemon off;'"

FROM base AS build

WORKDIR /app

COPY Client /app

RUN elm make -o /app/index.html Client/src/Main.elm

FROM nginx AS production

COPY Client/static /var/www/html/static
COPY Client/nginx/production.conf.template /etc/nginx/extra-conf/production.conf.template
COPY --from=build /app/index.html /var/www/html/index.html

CMD bash -c "envsubst '\$PROXY_NAME,\$PROXY_PORT,\$API_SERVER_URL,\$ELM_REACTOR_URL' < /etc/nginx/extra-conf/production.conf.template > /etc/nginx/conf.d/default.conf && nginx -g 'daemon off;'"