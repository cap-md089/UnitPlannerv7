-- ModalOpening.elm: Test to check that modals open and close properly
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


module Integration.Components.Modal.ModalOpening exposing (..)

import Browser
import Components.Button exposing (commonButton)
import Components.Modal as M exposing (Modal, ModalMsg, ModalOpenState(..), createModal, modalOpenState, startClosingModal, startOpeningModal)
import Element exposing (..)
import Html exposing (Html)


type Msg
    = OpenModal
    | StartClosingModal
    | ModalUpdate ModalMsg


type alias Model =
    { modal : Modal Msg
    }


init : () -> ( Model, Cmd Msg )
init () =
    ( { modal = createModal StartClosingModal ModalUpdate
      }
    , Cmd.none
    )


subscriptions : Model -> Sub Msg
subscriptions model =
    M.subscriptions model.modal |> Sub.map ModalUpdate


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        OpenModal ->
            ( { modal = startOpeningModal model.modal }, Cmd.none )

        StartClosingModal ->
            ( { modal = startClosingModal model.modal }, Cmd.none )

        ModalUpdate mu ->
            let
                handle modal =
                    ( { modal = modal }, Cmd.none )

                handlers =
                    { handleFinishClose = handle
                    , handleFinishOpen = handle
                    , handleOther = handle
                    }
            in
            M.update handlers mu model.modal


view : Model -> Html Msg
view model =
    let
        status =
            case modalOpenState model.modal of
                Open ->
                    text "Open"

                Opening ->
                    text "Opening"

                Closed ->
                    text "Closed"

                Closing ->
                    text "Closing"

        openModalButton =
            commonButton
                []
                { label = text "Open modal"
                , onPress = Just OpenModal
                }

        modalBody =
            column
                []
                [ text "Modal text"
                , commonButton
                    []
                    { label = text "Close modal"
                    , onPress = Just StartClosingModal
                    }
                ]

        modalEl =
            M.view model.modal modalBody
    in
    layout
        [ inFront modalEl ]
    <|
        column
            []
            [ status
            , openModalButton
            ]


main : Program () Model Msg
main =
    Browser.element
        { init = init
        , view = view
        , update = update
        , subscriptions = subscriptions
        }
