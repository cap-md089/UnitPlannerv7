-- Theme.elm: Used to keep styling consistent across the webmaster dashboard
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

module WebmasterDashboard.Theme exposing (..)

import Element exposing (..)
import Html exposing (Html)
import Loading exposing (LoaderType(..), defaultConfig)


standardLoader : Html msg
standardLoader =
    Loading.render BouncingBalls { defaultConfig | color = "#088", size = 40 } Loading.On


standardLoaderEl : Element msg
standardLoaderEl =
    html standardLoader


borderBottomColor : Color
borderBottomColor =
    rgb255 230 230 0
