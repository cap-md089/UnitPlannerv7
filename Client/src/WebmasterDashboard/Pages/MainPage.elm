-- MainPage.elm: Provides a nice overview dashboard
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


module WebmasterDashboard.Pages.MainPage exposing (..)

import Components.Button exposing (commonButton)
import Components.Events exposing (onEventNoBubble)
import Components.Modal as Modal exposing (Modal, ModalMsg, createModal, startClosingModal, startOpeningModal)
import Data.Unit exposing (Unit, unitDecoder)
import Element exposing (..)
import Element.Border as Border
import Element.Font as Font
import Element.Input as Input
import Element.Region as Region
import Html as H
import Html.Attributes as A
import Http
import Json.Decode as D
import Url exposing (Protocol(..))
import WebmasterDashboard.Theme exposing (borderBottomColor, standardLoaderEl)


type alias NewUnitForm =
    { id : String
    }


type alias Model =
    { units : Maybe (Result Http.Error (List Unit))
    , newUnitForm : NewUnitForm
    , newUnitEditModal : Modal Msg
    , sendCreateUnitCmd : Bool
    , newUnitResult : Maybe (Result Http.Error ())
    , newUnitResultsModal : Modal Msg
    }


type Msg
    = Ignore
    | LoadedUnits (Result Http.Error (List Unit))
    | CloseNewUnitForm
    | OpenNewUnitForm
    | UpdateNewUnitForm NewUnitForm
    | UnitEditModalUpdate ModalMsg
    | CreateUnit NewUnitForm
    | FinishCreatingUnit (Result Http.Error ())
    | CloseNewUnitResults
    | UnitResultModalUpdate ModalMsg


init : ( Model, Cmd Msg )
init =
    ( { units = Nothing
      , newUnitForm = { id = "" }
      , newUnitEditModal = createModal CloseNewUnitForm UnitEditModalUpdate
      , sendCreateUnitCmd = False
      , newUnitResult = Nothing
      , newUnitResultsModal = createModal CloseNewUnitForm UnitResultModalUpdate
      }
    , getUnits LoadedUnits
    )


getUnits : (Result Http.Error (List Unit) -> msg) -> Cmd msg
getUnits msg =
    Http.get
        { url = "/api"
        , expect = Http.expectJson msg (D.list unitDecoder)
        }


createUnit : NewUnitForm -> (Result Http.Error () -> msg) -> Cmd msg
createUnit unit msg =
    Http.post
        { url = "/api/" ++ unit.id
        , expect = Http.expectWhatever msg
        , body = Http.emptyBody
        }


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        Ignore ->
            ( model, Cmd.none )

        LoadedUnits res ->
            ( { model | units = Just res }, Cmd.none )

        CloseNewUnitForm ->
            ( { model | newUnitEditModal = startClosingModal model.newUnitEditModal }, Cmd.none )

        OpenNewUnitForm ->
            ( { model | newUnitForm = { id = "" }, newUnitEditModal = startOpeningModal model.newUnitEditModal }, Cmd.none )

        UpdateNewUnitForm newForm ->
            ( { model | newUnitForm = newForm }, Cmd.none )

        UnitEditModalUpdate modalUpdate ->
            let
                handleOther modal =
                    ( { model | newUnitEditModal = modal }, Cmd.none )

                handlers =
                    { handleFinishClose = handleOther
                    , handleFinishOpen = handleOther
                    , handleOther = handleOther
                    }
            in
            Modal.update handlers modalUpdate model.newUnitEditModal

        CreateUnit form ->
            ( { model | newUnitEditModal = Modal.startClosingModal model.newUnitEditModal, newUnitResult = Nothing }, createUnit form FinishCreatingUnit )

        FinishCreatingUnit res ->
            case res of
                Ok _ ->
                    ( { model
                        | newUnitResult = Just res
                        , units = Maybe.map (Result.map ((::) { id = model.newUnitForm.id })) model.units
                      }
                    , Cmd.none
                    )

                Err _ ->
                    ( { model | newUnitResultsModal = Modal.startOpeningModal model.newUnitResultsModal, newUnitResult = Just res }, Cmd.none )

        CloseNewUnitResults ->
            ( { model | newUnitResultsModal = Modal.startClosingModal model.newUnitResultsModal }, Cmd.none )

        UnitResultModalUpdate modalUpdate ->
            let
                handleOther modal =
                    ( { model | newUnitResultsModal = modal }, Cmd.none )

                handlers =
                    { handleFinishClose = handleOther
                    , handleFinishOpen = handleOther
                    , handleOther = handleOther
                    }
            in
            Modal.update handlers modalUpdate model.newUnitResultsModal


subscriptions : Model -> Sub Msg
subscriptions model =
    Sub.batch
        [ Modal.subscriptions model.newUnitEditModal |> Sub.map UnitEditModalUpdate
        , Modal.subscriptions model.newUnitResultsModal |> Sub.map UnitResultModalUpdate
        ]


widgetAttrs : List (Attribute Msg)
widgetAttrs =
    [ spacing 20
    , height shrink
    , width fill
    , padding 20
    , Border.color <| rgb 0 0 0
    , Border.width 1
    , Border.shadow
        { offset = ( 0, 0 )
        , size = 0
        , blur = 4
        , color = rgb 0 0 0
        }
    ]


viewUnitsWidget : List Unit -> Element Msg
viewUnitsWidget units =
    let
        unitRender unit =
            el
                [ Border.color <| rgb255 0 0 0
                , Border.shadow
                    { offset = ( 0, 0 )
                    , size = 0
                    , blur = 2
                    , color = rgb255 0 0 0
                    }
                , padding 10
                , width fill
                ]
            <|
                text unit.id

        renderedUnits =
            List.map unitRender units
    in
    column widgetAttrs <|
        [ el
            [ Region.heading 2
            , Font.size 24
            , Border.widthEach
                { bottom = 1
                , left = 0
                , right = 0
                , top = 0
                }
            , Border.color <| borderBottomColor
            , width fill
            ]
            (text "Unit listing")
        , commonButton
            []
            { label = text "New unit"
            , onPress = Just OpenNewUnitForm
            }
        ]
            ++ renderedUnits


view : Model -> Element Msg
view model =
    let
        unitsWidget =
            case model.units of
                Just (Ok units) ->
                    viewUnitsWidget units

                Just (Err _) ->
                    el widgetAttrs <|
                        text "There was a problem loading the main dashboard"

                Nothing ->
                    el widgetAttrs <|
                        el [ centerX, centerY ] <|
                            standardLoaderEl
    in
    row
        [ padding 30
        , width fill
        ]
        [ column
            [ width <| fillPortion 4 ]
            [ unitsWidget ]
        , column
            [ width <| fillPortion 4 ]
            []
        ]


viewUnitEditForm : Model -> Element Msg
viewUnitEditForm model =
    let
        form =
            model.newUnitForm
    in
    html <|
        H.form
            [ A.id "newUnitForm"
            , onEventNoBubble "submit" <| CreateUnit form
            ]
        <|
            [ layoutWith
                { options = [ noStaticStyleSheet ] }
                []
              <|
                column
                    [ padding 30
                    , width fill
                    , spacing 20
                    ]
                    [ el
                        [ Border.widthEach
                            { left = 0
                            , right = 0
                            , bottom = 1
                            , top = 0
                            }
                        , Border.color <| borderBottomColor
                        , padding 5
                        , width fill
                        , Font.center
                        ]
                        (text "Create new unit")
                    , Input.text
                        [ htmlAttribute <| A.id "new-unit-form-id-input"
                        ]
                        { text = form.id
                        , placeholder = Nothing
                        , onChange = \id -> UpdateNewUnitForm { form | id = id }
                        , label = Input.labelLeft [] (text "New ID")
                        }
                    , row
                        [ alignRight
                        , spacing 10
                        ]
                        [ commonButton
                            [ ]
                            { label = text "Cancel"
                            , onPress = Just CloseNewUnitForm
                            }
                        , commonButton
                            [ htmlAttribute <| A.form "newUnitForm"
                            , htmlAttribute <| A.class "new-unit-form-submit-button"
                            ]
                            { label = text "Submit"
                            , onPress = Just <| CreateUnit form
                            }
                        ]
                    ]
            ]


viewUnitResultsModal : Model -> Element Msg
viewUnitResultsModal _ =
    column
        [ height fill
        , width fill
        , centerX
        , centerY
        , padding 20
        ]
        [ el
            [ Border.widthEach
                { left = 0
                , right = 0
                , bottom = 1
                , top = 0
                }
            , Border.color <| borderBottomColor
            , padding 5
            , width fill
            , Font.center
            ]
            (text "Create new unit")
        , text "An unknown error occurred"
        ]


modals : Model -> List (Element Msg)
modals model =
    [ Modal.view model.newUnitEditModal (viewUnitEditForm model)
    , Modal.view model.newUnitResultsModal (viewUnitResultsModal model)
    ]
