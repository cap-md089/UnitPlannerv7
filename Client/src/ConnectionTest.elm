-- Main.elm: Acts as the main page for the user facing website
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


module ConnectionTest exposing (..)

import Browser
import Data.WeatherForecast exposing (..)
import Html exposing (Html, button, div, node, span, text)
import Html.Attributes exposing (attribute, style)
import Html.Events exposing (onClick)
import Http exposing (Error(..))
import Json.Decode as D
import String exposing (fromInt)



-- MAIN


main : Program () Model Msg
main =
    Browser.document
        { init = init
        , view = view
        , update = update
        , subscriptions = subscriptions
        }



-- MODEL


type alias Model =
    { data : Maybe (Result Error (List WeatherForecast))
    , authenticationGood : Maybe Bool
    , capwatchGood : Maybe Bool
    , filesGood : Maybe Bool
    , graphGood : Maybe Bool
    }


init : () -> ( Model, Cmd Msg )
init _ =
    ( { data = Maybe.Nothing
      , authenticationGood = Maybe.Nothing
      , capwatchGood = Maybe.Nothing
      , filesGood = Maybe.Nothing
      , graphGood = Maybe.Nothing
      }
    , getWeatherForecast ReceivedWeatherForecast
    )


getWeatherForecast : (Result Error (List WeatherForecast) -> msg) -> Cmd msg
getWeatherForecast msg =
    Http.get
        { url = "/api/WeatherForecast"
        , expect = Http.expectJson msg (D.list weatherForecastDecoder)
        }



-- UPDATE


type Msg
    = ReceivedWeatherForecast (Result Http.Error (List WeatherForecast))
    | SendTestAuthenticationMsg
    | SendTestCapwatchMsg
    | SendTestFilesMsg
    | SendTestGraphMsg
    | ReceiveTestAuthenticationMsg (Result Error Bool)
    | ReceiveTestCapwatchMsg (Result Error Bool)
    | ReceiveTestFilesMsg (Result Error Bool)
    | ReceiveTestGraphMsg (Result Error Bool)


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        ReceivedWeatherForecast res ->
            ( { model | data = Maybe.Just res }, Cmd.none )

        SendTestAuthenticationMsg ->
            ( model, testAuthenticationMsg )

        SendTestCapwatchMsg ->
            ( model, testCapwatchMsg )

        SendTestFilesMsg ->
            ( model, testFilesMsg )

        SendTestGraphMsg ->
            ( model, testGraphMsg )

        ReceiveTestAuthenticationMsg (Result.Ok val) ->
            ( { model | authenticationGood = Maybe.Just val }, Cmd.none )

        ReceiveTestAuthenticationMsg _ ->
            ( { model | authenticationGood = Maybe.Just False }, Cmd.none )

        ReceiveTestCapwatchMsg (Result.Ok val) ->
            ( { model | capwatchGood = Maybe.Just val }, Cmd.none )

        ReceiveTestCapwatchMsg _ ->
            ( { model | capwatchGood = Maybe.Just False }, Cmd.none )

        ReceiveTestFilesMsg (Result.Ok val) ->
            ( { model | filesGood = Maybe.Just val }, Cmd.none )

        ReceiveTestFilesMsg _ ->
            ( { model | filesGood = Maybe.Just False }, Cmd.none )

        ReceiveTestGraphMsg (Result.Ok val) ->
            ( { model | graphGood = Maybe.Just val }, Cmd.none )

        ReceiveTestGraphMsg _ ->
            ( { model | graphGood = Maybe.Just False }, Cmd.none )


sendTestRequest : (Result Error Bool -> msg) -> Int -> Cmd msg
sendTestRequest msg id =
    Http.get
        { url = "/api/ConnectionTest?clientId=" ++ fromInt id
        , expect = Http.expectJson msg D.bool
        }


testAuthenticationMsg : Cmd Msg
testAuthenticationMsg =
    sendTestRequest ReceiveTestAuthenticationMsg 0


testCapwatchMsg : Cmd Msg
testCapwatchMsg =
    sendTestRequest ReceiveTestCapwatchMsg 1


testFilesMsg : Cmd Msg
testFilesMsg =
    sendTestRequest ReceiveTestFilesMsg 2


testGraphMsg : Cmd Msg
testGraphMsg =
    sendTestRequest ReceiveTestGraphMsg 3



-- SUBSCRIPTIONS


subscriptions : Model -> Sub Msg
subscriptions _ =
    Sub.none



-- VIEW


view : Model -> Browser.Document Msg
view model =
    let
        display =
            case model.data of
                Just (Ok _) ->
                    div [] [ text "done" ]

                Just (Err _) ->
                    div [] [ text "error" ]

                Nothing ->
                    div [] [ text "loading" ]

        pageStyle =
            node "link"
                [ attribute "rel" "stylesheet"
                , attribute "href" "/static/reset.css"
                ]
                []
    in
    { title = "page"
    , body =
        [ pageStyle
        , display
        , testButton SendTestAuthenticationMsg model.authenticationGood
        , testButton SendTestCapwatchMsg model.capwatchGood
        , testButton SendTestFilesMsg model.filesGood
        , testButton SendTestGraphMsg model.graphGood
        ]
    }


testButton : msg -> Maybe Bool -> Html msg
testButton msg valid =
    let
        display =
            case valid of
                Just True ->
                    span [] [ text "good!" ]

                Just False ->
                    span [] [ text "not good!" ]

                Nothing ->
                    span [] [ text "loading" ]
    in
    div
        [ style "border" "1px solid red"
        , style "padding" "10px"
        ]
        [ button [ onClick msg ] [ text "Test connection" ]
        , display
        ]
