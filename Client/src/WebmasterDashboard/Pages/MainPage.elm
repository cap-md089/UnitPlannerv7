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

import Components.Button exposing (commonButton, disabledButton)
import Components.Modal as Modal exposing (Modal, ModalMsg, createModal, startClosingModal, startOpeningModal)
import Data.Unit exposing (Account, accountDecoder)
import Element exposing (..)
import Element.Border as Border
import Element.Font as Font
import Element.Input as Input
import Element.Region as Region
import Html as H
import Html.Attributes as A
import Http
import Json.Decode as D
import Json.Decode.Pipeline as P
import Json.Encode as E
import Url exposing (Protocol(..))
import WebmasterDashboard.Theme exposing (borderBottomColor, standardLoaderEl)
import Components.Events exposing (onEventNoBubble)
import Data.Unit exposing (accountID)
import Data.Unit exposing (Account(..))
import Data.Unit exposing (CAPActivityBody)
import Data.Unit exposing (CAPWingBody)
import Data.Unit exposing (CAPGroupBody)
import Data.Unit exposing (CAPSquadronBody)
import Data.Unit exposing (capActivityDecoder)
import Data.Unit exposing (capWingDecoder)
import Data.Unit exposing (capGroupDecoder)
import Data.Unit exposing (capSquadronDecoder)


type alias NewSquadronForm =
    { wingSelection : Maybe String
    , groupSelection : Maybe String
    }

type alias NewSquadronRequest =
    { unitId : String
    , wingId : String
    , groupId : String
    }

type alias NewGroupForm =
    { wingSelection : Maybe String
    }

type alias NewGroupRequest =
    { unitId : String
    , wingId : String
    }

type alias NewWingForm =
    {
    }

type alias NewWingRequest =
    { unitId : String
    }

type SpecificNewUnitForm
    = NewWing NewWingForm
    | NewGroup NewGroupForm
    | NewSquadron NewSquadronForm

type NewUnitRequest
    = NewWingR NewWingRequest
    | NewGroupR NewGroupRequest
    | NewSquadronR NewSquadronRequest

validateForm : String -> SpecificNewUnitForm -> Maybe NewUnitRequest
validateForm unitId f =
    let
        unitIdMaybe =
            if String.isEmpty unitId
                then Nothing
                else Just unitId
    in
    case f of
        NewWing _ ->
            unitIdMaybe
                |> Maybe.map NewWingRequest
                |> Maybe.map NewWingR

        NewGroup groupForm ->
            Maybe.map2 NewGroupRequest unitIdMaybe groupForm.wingSelection
                |> Maybe.map NewGroupR

        NewSquadron squadronForm ->
            Maybe.map3 NewSquadronRequest unitIdMaybe squadronForm.wingSelection squadronForm.groupSelection
                |> Maybe.map NewSquadronR

type alias NewUnitForm =
    { id : String
    , unitSpecifics : SpecificNewUnitForm
    }

type alias UnitRequestResult =
    { activityAccounts : List CAPActivityBody
    , wingAccounts : List CAPWingBody
    , groupAccounts : List CAPGroupBody
    , squadronAccounts : List CAPSquadronBody
    }

type alias Model =
    { units : Maybe (Result Http.Error (List Account))
    , newUnitForm : NewUnitForm
    , newUnitEditModal : Modal Msg
    , sendCreateUnitCmd : Bool
    , newUnitResult : Maybe (Result Http.Error Account)
    , newUnitResultsModal : Modal Msg
    }


type Msg
    = Ignore
    | LoadedUnits (Result Http.Error (List Account))
    | CloseNewUnitForm
    | OpenNewUnitForm
    | UpdateNewUnitForm NewUnitForm
    | UnitEditModalUpdate ModalMsg
    | CreateUnit NewUnitRequest
    | DisabledCreateUnit
    | FinishCreatingUnit (Result Http.Error Account)
    | CloseNewUnitResults
    | UnitResultModalUpdate ModalMsg

init : ( Model, Cmd Msg )
init =
    ( { units = Nothing
      , newUnitForm =
        { id = ""
        , unitSpecifics = NewSquadron <|
            { wingSelection = Nothing
            , groupSelection = Nothing
            }
        }
      , newUnitEditModal = createModal CloseNewUnitForm UnitEditModalUpdate
      , sendCreateUnitCmd = False
      , newUnitResult = Nothing
      , newUnitResultsModal = createModal CloseNewUnitResults UnitResultModalUpdate
      }
    , getUnits LoadedUnits
    )


getUnits : (Result Http.Error (List Account) -> msg) -> Cmd msg
getUnits msg =
    let
        resultDecoder =
            D.succeed UnitRequestResult
                |> P.required "activityAccounts" (D.list capActivityDecoder)
                |> P.required "wingAccounts" (D.list capWingDecoder)
                |> P.required "groupAccounts" (D.list capGroupDecoder)
                |> P.required "squadronAccounts" (D.list capSquadronDecoder)

        msgDecoder =
            resultDecoder
                |> D.map (\res ->
                    List.map CAPActivity res.activityAccounts ++
                    List.map CAPWing res.wingAccounts ++
                    List.map CAPGroup res.groupAccounts ++
                    List.map CAPSquadron res.squadronAccounts) 
    in
    Http.get
        { url = "/api/unit"
        , expect = Http.expectJson msg msgDecoder
        }

createWing : NewWingRequest -> (Result Http.Error Account -> msg) -> Cmd msg
createWing unit msg =
    Http.post
        { url = "/api/unit/wing"
        , expect = Http.expectJson msg accountDecoder
        , body = Http.jsonBody <|
            E.object
                [ ("unitId", E.string unit.unitId)
                , ("orgIds", E.list E.int [])
                ]
        }

createGroup : NewGroupRequest -> (Result Http.Error Account -> msg) -> Cmd msg
createGroup unit msg =
    Http.post
        { url = "/api/unit/group"
        , expect = Http.expectJson msg accountDecoder
        , body = Http.jsonBody <|
            E.object
                [ ("unitId", E.string unit.unitId)
                , ("wingId", E.string unit.wingId)
                , ("orgIds", E.list E.int [])
                ]
        }


createSquadron : NewSquadronRequest -> (Result Http.Error Account -> msg) -> Cmd msg
createSquadron unit msg =
    Http.post
        { url = "/api/unit/squadron"
        , expect = Http.expectJson msg accountDecoder
        , body = Http.jsonBody <|
            E.object
                [ ("unitId", E.string unit.unitId)
                , ("wingId", E.string unit.wingId)
                , ("groupId", E.string unit.groupId)
                , ("orgIds", E.list E.int [])
                ]
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
            ( { model | newUnitForm = { id = "", unitSpecifics = NewSquadron { wingSelection = Nothing, groupSelection = Nothing } }, newUnitEditModal = startOpeningModal model.newUnitEditModal }, Cmd.none )

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
            case form of
                NewWingR wingForm ->
                    ( { model | newUnitEditModal = Modal.startClosingModal model.newUnitEditModal, newUnitResult = Nothing }, createWing wingForm FinishCreatingUnit )

                NewGroupR groupForm ->
                    ( { model | newUnitEditModal = Modal.startClosingModal model.newUnitEditModal, newUnitResult = Nothing }, createGroup groupForm FinishCreatingUnit )

                NewSquadronR squadronForm ->
                    ( { model | newUnitEditModal = Modal.startClosingModal model.newUnitEditModal, newUnitResult = Nothing }, createSquadron squadronForm FinishCreatingUnit )

        DisabledCreateUnit ->
            ( model, Cmd.none )

        FinishCreatingUnit res ->
            case res of
                Ok acc ->
                    ( { model
                        | newUnitResult = Just res
                        , units = Maybe.map (Result.map ((::) acc)) model.units
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


viewUnitsWidget : List Account -> Element Msg
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
                (text <| accountID unit)

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

type NewUnitFormSelect
    = SelectWing
    | SelectGroup
    | SelectSquadron

viewUnitEditForm : Model -> Element Msg
viewUnitEditForm model =
    let
        form =
            model.newUnitForm

        initNewForm selection =
            case selection of
                SelectSquadron ->
                    UpdateNewUnitForm { form | unitSpecifics = NewSquadron { wingSelection = Nothing, groupSelection = Nothing } }

                SelectGroup ->
                    UpdateNewUnitForm { form | unitSpecifics = NewGroup { wingSelection = Nothing } }

                SelectWing ->
                    UpdateNewUnitForm { form | unitSpecifics = NewWing {} }

        validatedForm =
            validateForm form.id form.unitSpecifics
                |> Maybe.map CreateUnit

        submitButton =
            case validatedForm of
                Just msg ->
                    commonButton
                        [ htmlAttribute <| A.form "newUnitForm"
                        , htmlAttribute <| A.class "new-unit-form-submit-button"
                        ]
                        { label = text "Submit"
                        , onPress = Just msg
                        }


                Nothing ->
                    disabledButton
                        [ htmlAttribute <| A.form "newUnitForm"
                        , htmlAttribute <| A.class "new-unit-form-submit-button"
                        ]
                        { label = text "Submit"
                        , onPress = Just DisabledCreateUnit
                        }

        formStart = 
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
                , label = Input.labelAbove [] (text "New ID")
                }
            , Input.radio
                [ htmlAttribute <| A.id "new-unit-form-radio-select"
                ]
                { selected = Just <|
                    case form.unitSpecifics of
                        NewSquadron _ ->
                            SelectSquadron

                        NewGroup _ ->
                            SelectGroup
                        
                        NewWing _ ->
                            SelectWing
                , label = Input.labelAbove [] (text "Select unit type")
                , options = [ Input.option SelectSquadron (text "Squadron")
                            , Input.option SelectGroup (text "Group")
                            , Input.option SelectWing (text "Wing")
                            ]
                , onChange = initNewForm
                }
            ]

        filterWing unit =
            case unit of
                CAPWing body ->
                    Just body

                _ ->
                    Nothing

        filterGroup wingId unit =
            case unit of
                CAPGroup body ->
                    if body.wingId == wingId then
                        Just body
                    else
                        Nothing
                
                _ ->
                    Nothing

        formMiddle =
            case form.unitSpecifics of
                NewWing _ ->
                    []

                NewGroup groupForm ->
                    [ Input.radio
                        [ spacing <| 5 ]
                        { selected = case (model.units, groupForm.wingSelection) of
                            (Just (Ok units), Just wingId) ->
                                units
                                    |> List.filter (\unit -> accountID (unit) == wingId)
                                    |> List.head
                                    |> Maybe.map accountID

                            (_, _) ->
                                Nothing
                        , label = Input.labelAbove [] (text "Select wing")
                        , options = case model.units of
                            Just (Ok units) ->
                                units
                                    |> List.filterMap filterWing
                                    |> List.map (\unit -> Input.option unit.id (text unit.id))

                            _ ->
                                []
                        , onChange = \newWingId -> UpdateNewUnitForm <|
                            { form | unitSpecifics = NewGroup <| { groupForm | wingSelection = Just newWingId } }
                        }
                    ]

                NewSquadron squadronForm ->
                    let
                        wingSelection =
                            [ Input.radio []
                                { selected = case (model.units, squadronForm.wingSelection) of
                                    (Just (Ok units), Just wingId) ->
                                        units
                                            |> List.filter (\unit -> accountID (unit) == wingId)
                                            |> List.head
                                            |> Maybe.map accountID

                                    (_, _) ->
                                        Nothing
                                , label = Input.labelAbove [] (text "Select wing")
                                , options = case model.units of
                                    Just (Ok units) ->
                                        units
                                            |> List.filterMap filterWing
                                            |> List.map (\unit -> Input.option unit.id (text unit.id))

                                    _ ->
                                        []
                                , onChange = \newWingId -> UpdateNewUnitForm <|
                                    { form | unitSpecifics = NewSquadron <| { squadronForm | wingSelection = Just newWingId } }
                                }
                            ]
                        
                        groupSelection =
                            case squadronForm.wingSelection of
                                Nothing ->
                                    []

                                Just wingId ->
                                    [ Input.radio []
                                        { selected = case (model.units, squadronForm.groupSelection) of
                                            (Just (Ok units), Just groupId) ->
                                                units
                                                    |> List.filter (\unit -> accountID (unit) == groupId)
                                                    |> List.head
                                                    |> Maybe.map accountID

                                            (_, _) ->
                                                Nothing
                                        , label = Input.labelAbove [] (text "Select group")
                                        , options = case model.units of
                                            Just (Ok units) ->
                                                units
                                                    |> List.filterMap (filterGroup wingId)
                                                    |> List.map (\unit -> Input.option unit.id (text unit.id))

                                            _ ->
                                                []
                                        , onChange = \newWingId -> UpdateNewUnitForm <|
                                            { form | unitSpecifics = NewSquadron <| { squadronForm | groupSelection = Just newWingId } }
                                        }

                                    ]

                    in
                    wingSelection ++ groupSelection

        formEnd =
            [ row
                [ alignRight
                , spacing 10
                ]
                [ commonButton
                    [ ]
                    { label = text "Cancel"
                    , onPress = Just CloseNewUnitForm
                    }
                , submitButton
                ]
            ]
    in
    html <|
        H.form
            [ A.id "newUnitForm"
            , onEventNoBubble "submit" DisabledCreateUnit
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
                    (formStart ++ formMiddle ++ formEnd)
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
