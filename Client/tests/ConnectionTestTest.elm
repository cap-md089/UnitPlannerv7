-- ConnectionTestTest.elm: Tests the connection test module, before the integration test
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

module ConnectionTestTest exposing (..)

import ConnectionTest exposing (..)
import Expect
import Html exposing (button, div, span, text)
import Html.Attributes exposing (style)
import Html.Events exposing (onClick)
import Test exposing (..)


type TestMsg
    = SendMsg


suite : Test
suite =
    let
        goodButton =
            div
                [ style "border" "1px solid red"
                , style "padding" "10px"
                ]
                [ button [ onClick SendMsg ] [ text "Test connection" ]
                , span [] [ text "good!" ]
                ]
    in
    describe "Connection test page"
        [ test "makes a proper button for testing" <|
            \_ ->
                testButton SendMsg (Just True)
                    |> Expect.equal goodButton
        ]