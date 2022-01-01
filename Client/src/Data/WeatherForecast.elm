-- WeatherForecast.elm: Provides data structures and JSON decoders for interacting with an example API
--
-- Copyright (C) 2021 Andrew Rioux
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


module Data.WeatherForecast exposing (..)

import Iso8601 as I
import Json.Decode as D exposing (Decoder, float, nullable, string)
import Json.Decode.Pipeline exposing (required)
import Time exposing (Posix)


type alias WeatherForecast =
    { date : Posix
    , temperatureC : Float
    , summary : Maybe String
    }


weatherForecastDecoder : Decoder WeatherForecast
weatherForecastDecoder =
    D.succeed WeatherForecast
        |> required "date" I.decoder
        |> required "temperatureC" float
        |> required "summary" (nullable string)
