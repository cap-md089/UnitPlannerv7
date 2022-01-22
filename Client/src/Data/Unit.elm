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

module Data.Unit exposing (Account(..), CAPActivityBody, CAPWingBody, CAPGroupBody, CAPSquadronBody, accountID, accountDecoder, accountEncoder, capActivityDecoder, capWingDecoder, capGroupDecoder, capSquadronDecoder)

import Json.Decode as D exposing (Decoder)
import Json.Decode.Pipeline exposing (required)
import Json.Encode as E

type alias CAPActivityBody =
    { id : String
    , hostId : String
    }

type alias CAPWingBody =
    { id : String
    }

type alias CAPGroupBody =
    { id : String
    , wingId : String
    }

type alias CAPSquadronBody =
    { id : String
    , wingId : String
    , groupId : String
    }

type Account
    = CAPActivity CAPActivityBody
    | CAPWing CAPWingBody
    | CAPGroup CAPGroupBody
    | CAPSquadron CAPSquadronBody

accountID : Account -> String
accountID account =
    case account of
        CAPActivity body ->
            body.id

        CAPWing body ->
            body.id

        CAPGroup body ->
            body.id

        CAPSquadron body ->
            body.id

capActivityDecoder : Decoder CAPActivityBody
capActivityDecoder =
    D.succeed CAPActivityBody
        |> required "id" D.string
        |> required "hostId" D.string

capWingDecoder : Decoder CAPWingBody
capWingDecoder = 
    D.succeed CAPWingBody
        |> required "id" D.string

capGroupDecoder : Decoder CAPGroupBody
capGroupDecoder =
    D.succeed CAPGroupBody
        |> required "id" D.string
        |> required "wingId" D.string

capSquadronDecoder : Decoder CAPSquadronBody
capSquadronDecoder =
    D.succeed CAPSquadronBody
        |> required "id" D.string
        |> required "wingId" D.string
        |> required "groupId" D.string

decideOnDecoder : String -> Decoder Account
decideOnDecoder t =
    case t of
        "CAPActivity" ->
            capActivityDecoder
                |> D.map CAPActivity

        "CAPWing" ->
            capWingDecoder
                |> D.map CAPWing

        "CAPGroup" ->
            capGroupDecoder
                |> D.map CAPGroup

        "CAPSquadron" ->
            capSquadronDecoder
                |> D.map CAPSquadron

        _ ->
            D.fail <|
                "Unknown account type: " ++ t


accountDecoder : Decoder Account
accountDecoder =
    D.field "type" D.string
        |> D.andThen decideOnDecoder

accountEncoder : Account -> E.Value
accountEncoder unit =
    case unit of
        CAPActivity body ->
            E.object
                [ ("id", E.string body.id)
                , ("hostId", E.string body.hostId)
                ]

        CAPWing body ->
            E.object
                [ ("id", E.string body.id)
                ]

        CAPGroup body ->
            E.object
                [ ("id", E.string body.id)
                , ("wingId", E.string body.wingId)
                ]

        CAPSquadron body ->
            E.object
                [ ("id", E.string body.id)
                , ("wingId", E.string body.wingId)
                , ("groupId", E.string body.groupId)
                ]