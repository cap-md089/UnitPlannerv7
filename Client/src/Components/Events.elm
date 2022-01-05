-- Events.elm: Used to help with preventing bubbling for HTML events
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

module Components.Events exposing (..)

import Html exposing (Attribute)
import Html.Events exposing (custom)
import Json.Decode exposing (succeed)
import Element exposing (htmlAttribute)


onEventNoBubble : String -> msg -> Attribute msg
onEventNoBubble event msg =
    custom event (succeed { message = msg, stopPropagation = True, preventDefault = True })

onEventNoBubbleEl : String -> msg -> Element.Attribute msg
onEventNoBubbleEl event msg = 
    onEventNoBubble event msg
        |> htmlAttribute
