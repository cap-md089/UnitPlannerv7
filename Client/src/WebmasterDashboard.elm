-- WebmasterDashboard.elm: Provides the entrypoint for the webmaster dashboard
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

module WebmasterDashboard exposing (..)

import Browser exposing (Document)
import Browser.Navigation as Nav
import Element exposing (..)
import Element.Background as Background
import Element.Border as Border
import Element.Font as Font
import Url
import WebmasterDashboard.Pages.MainPage as MP
import WebmasterDashboard.Routes exposing (Route(..), parseRoute)


main : Program () Model Msg
main =
    Browser.application
        { init = init
        , view = view
        , update = update
        , subscriptions = subscriptions
        , onUrlChange = UrlChanged
        , onUrlRequest = LinkClicked
        }



-- MODEL


type PageModel
    = MainPage MP.Model
    | NoPage


type alias Model =
    { key : Nav.Key
    , currentPage : PageModel
    }


init : () -> Url.Url -> Nav.Key -> ( Model, Cmd Msg )
init _ url key =
    initialModelFromUrl key <| parseRoute url


initialModelFromUrl : Nav.Key -> Route -> ( Model, Cmd Msg )
initialModelFromUrl key route =
    let
        emptyModel =
            { key = key
            , currentPage = NoPage
            }
    in
    case route of
        Main ->
            MP.init |> updateWith MainPage MainPageUpdate emptyModel

        PageNotFound ->
            ( emptyModel, Cmd.none )



-- UPDATE


type Msg
    = LinkClicked Browser.UrlRequest
    | UrlChanged Url.Url
    | MainPageUpdate MP.Msg


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case ( msg, model.currentPage ) of
        ( LinkClicked urlRequest, _ ) ->
            case urlRequest of
                Browser.Internal url ->
                    ( model, Nav.pushUrl model.key (Url.toString url) )

                Browser.External href ->
                    ( model, Nav.load href )

        ( UrlChanged url, _ ) ->
            modelFromUrlChange (parseRoute url) model

        ( MainPageUpdate mp, MainPage page ) ->
            MP.update mp page |> updateWith MainPage MainPageUpdate model

        ( _, _ ) ->
            ( model, Cmd.none )


modelFromUrlChange : Route -> Model -> ( Model, Cmd Msg )
modelFromUrlChange route model =
    case route of
        Main ->
            MP.init |> updateWith MainPage MainPageUpdate model

        PageNotFound ->
            ( { model | currentPage = NoPage }, Cmd.none )


updateWith : (subModel -> PageModel) -> (subMsg -> Msg) -> Model -> ( subModel, Cmd subMsg ) -> ( Model, Cmd Msg )
updateWith toModel toMsg model ( subModel, subCmd ) =
    ( { model | currentPage = toModel subModel }
    , Cmd.map toMsg subCmd
    )



-- SUBSCRIPTIONS


subscriptions : Model -> Sub Msg
subscriptions model =
    case model.currentPage of
        MainPage state ->
            MP.subscriptions state
                |> Sub.map MainPageUpdate

        NoPage ->
            Sub.none



-- VIEW


mapModal : (msg -> superMsg) -> Element msg -> Attribute superMsg
mapModal transform modal =
    modal
        |> map transform
        |> inFront


view : Model -> Document Msg
view model =
    let
        sideLinkAttrs isSelected =
            let
                unselectedAttrs =
                    [ Font.color <| rgb255 255 255 255
                    , Font.size 28
                    , width fill
                    , padding 10
                    ]

                selectedAttrs =
                    [ Background.color <| rgb255 0 100 100
                    , Border.rounded 5
                    ]
            in
            if isSelected then
                unselectedAttrs ++ selectedAttrs

            else
                unselectedAttrs

        sideLink url label isSelected =
            el
                [ width fill
                , padding 10
                ]
            <|
                link
                    (sideLinkAttrs isSelected)
                    { url = url
                    , label = text label
                    }

        sidebar =
            column
                [ width <| fillPortion 1
                , height fill
                , Background.color <| rgb255 0 70 70
                , spacing 10
                ]
                [ sideLink "/" "Home" <|
                    case model.currentPage of
                        MainPage _ ->
                            True

                        _ ->
                            False
                ]

        contentBody =
            case model.currentPage of
                MainPage s ->
                    MP.view s |> Element.map MainPageUpdate

                NoPage ->
                    el
                        [ width <| fillPortion 8
                        , height fill
                        , padding 30
                        ]
                        (text "Could not find page")

        modals =
            case model.currentPage of
                MainPage s ->
                    MP.modals s
                        |> List.map (mapModal MainPageUpdate)

                NoPage ->
                    []

        body =
            layout
                modals
            <|
                row
                    [ height fill
                    , width fill
                    ]
                    [ sidebar
                    , el [ width <| fillPortion 8, height fill ] contentBody
                    ]
    in
    { body = [ body ]
    , title = "UnitPlanner Webmaster Dashboard"
    }
