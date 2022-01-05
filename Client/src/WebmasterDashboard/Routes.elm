-- Routes.elm: Parses the current page URL to return a page route
--
-- Copyright (C) 2022 Andrew Rioux
-- 
-- This program is free software: you can redistribute it and/or modify
-- it under the terms of the GNU Affero General Public License as
-- published by the Free Software Foundation, either version 3 of the
-- License, or (at your option) any later version.
-- 
-- This program is distributed in the hope that it will be useful,
-- but WITHOUT ANY WARRANTY; without even the implied warranty of
-- MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
-- GNU Affero General Public License for more details.
-- 
-- You should have received a copy of the GNU Affero General Public License
-- along with this program.  If not, see <http://www.gnu.org/licenses/>.

module WebmasterDashboard.Routes exposing (..)

import Url
import Url.Parser as P


type Route
    = Main
    | PageNotFound


routeParser : P.Parser (Route -> a) a
routeParser =
    P.oneOf
        [ P.map Main P.top
        ]


parseRoute : Url.Url -> Route
parseRoute url =
    P.parse routeParser url
        |> Maybe.withDefault PageNotFound
