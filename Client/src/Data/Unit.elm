-- Unit.elm: Data management functions for a CAP unit data structure
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

module Data.Unit exposing (..)

import Json.Decode as D exposing (Decoder, string)
import Json.Decode.Pipeline exposing (required)
import Json.Encode as E


type alias Unit =
    { id : String
    }


unitDecoder : Decoder Unit
unitDecoder =
    D.succeed Unit
        |> required "id" string


unitEncoder : Unit -> E.Value
unitEncoder unit =
    E.object
        [ ( "id", E.string unit.id )
        ]
