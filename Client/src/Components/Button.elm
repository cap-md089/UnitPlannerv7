-- Button.elm: Standard button used across all pages
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

module Components.Button exposing (..)

import Element exposing (..)
import Element.Background as Background
import Element.Border as Border
import Element.Font as Font
import Element.Input exposing (button)
import Html.Attributes exposing (class)
import Html exposing (Html)


commonButton : List (Attribute msg) -> { label : Element msg, onPress : Maybe msg } -> Element msg
commonButton attrs =
    let
        commonAttrs =
            [ padding 10
            , Border.rounded 5
            , Background.color <| rgb255 0 0 180
            , Font.color <| rgb255 255 255 255
            , htmlAttribute <| class "button"
            ]
    in
    button <| commonAttrs ++ attrs

disabledButton : List (Attribute msg) -> { label : Element msg, onPress : Maybe msg } -> Element msg
disabledButton attrs =
    let
        commonAttrs =
            [ padding 10
            , Border.rounded 5
            , Background.color <| rgb255 120 120 120
            , Font.color <| rgb255 255 255 255
            , htmlAttribute <| class "button"
            ]
    in
    button <| commonAttrs ++ attrs


main : Html a
main =
    layout [] <|
        commonButton [] { label = text "I do nothing", onPress = Nothing }
