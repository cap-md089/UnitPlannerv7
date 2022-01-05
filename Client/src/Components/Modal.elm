-- Modal.elm: Basic modal that can be used in this project
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


module Components.Modal exposing (Modal, ModalConfig, ModalMsg, ModalOpenState(..), createModal, createOpenModal, isClosed, modalOpenState, startClosingModal, startOpeningModal, subscriptions, update, view)

import Components.Animation exposing (Animation, AnimationMsg, animationSubscription, createAnimation, currentPosition, easeInCubic, easeOutCubic, startAnimation, updateAnimation)
import Element exposing (..)
import Element.Background as Background
import Element.Border as Border
import Html.Attributes exposing (class)
import Components.Events exposing (onEventNoBubbleEl)


type alias ModalConfig msg =
    { startClosingMsg : msg
    , updateMsg : ModalMsg -> msg
    }


type ModalOpenState
    = Closed
    | Opening
    | Open
    | Closing


type alias ModalState =
    { openAnimation : Animation
    , closeAnimation : Animation
    , openState : ModalOpenState
    }


type Modal msg
    = Modal (ModalConfig msg) ModalState


createModal : msg -> (ModalMsg -> msg) -> Modal msg
createModal startClosingMsg updateMsg =
    Modal
        { startClosingMsg = startClosingMsg
        , updateMsg = updateMsg
        }
        { openAnimation = createAnimation easeOutCubic 300
        , closeAnimation = createAnimation easeInCubic 200
        , openState = Closed
        }


createOpenModal : msg -> (ModalMsg -> msg) -> Modal msg
createOpenModal startClosingMsg updateMsg =
    Modal
        { startClosingMsg = startClosingMsg
        , updateMsg = updateMsg
        }
        { openAnimation = createAnimation easeOutCubic 300
        , closeAnimation = createAnimation easeInCubic 200
        , openState = Open
        }


type ModalMsg
    = AnimationUpdate AnimationMsg
    | Ignore


subscriptions : Modal msg -> Sub ModalMsg
subscriptions (Modal _ state) =
    case state.openState of
        Closed ->
            Sub.none

        Open ->
            Sub.none

        Opening ->
            animationSubscription state.openAnimation |> Sub.map AnimationUpdate

        Closing ->
            animationSubscription state.closeAnimation |> Sub.map AnimationUpdate


update :
    { handleFinishClose : Modal msg -> ( model, Cmd msg )
    , handleFinishOpen : Modal msg -> ( model, Cmd msg )
    , handleOther : Modal msg -> ( model, Cmd msg )
    }
    -> ModalMsg
    -> Modal msg
    -> ( model, Cmd msg )
update { handleFinishClose, handleFinishOpen, handleOther } msg (Modal conf state) =
    case msg of
        AnimationUpdate animUpdate ->
            case state.openState of
                Closed ->
                    Modal conf state |> handleOther

                Open ->
                    Modal conf state |> handleOther

                Closing ->
                    let
                        handleFinish anim =
                            Modal conf { state | closeAnimation = anim, openState = Closed } |> handleFinishClose

                        handleProgress anim =
                            Modal conf { state | closeAnimation = anim } |> handleOther

                        handlers =
                            { handleAnimationFinish = handleFinish, handleAnimationProgress = handleProgress }
                    in
                    updateAnimation handlers animUpdate state.closeAnimation

                Opening ->
                    let
                        handleFinish anim =
                            Modal conf { state | openAnimation = anim, openState = Open } |> handleFinishOpen

                        handleProgress anim =
                            Modal conf { state | openAnimation = anim } |> handleOther

                        handlers =
                            { handleAnimationFinish = handleFinish, handleAnimationProgress = handleProgress }
                    in
                    updateAnimation handlers animUpdate state.openAnimation

        Ignore ->
            Modal conf state |> handleOther


startOpeningModal : Modal msg -> Modal msg
startOpeningModal (Modal conf state) =
    { state | openState = Opening, openAnimation = startAnimation state.openAnimation }
        |> Modal conf


startClosingModal : Modal msg -> Modal msg
startClosingModal (Modal conf state) =
    { state | openState = Closing, closeAnimation = startAnimation state.closeAnimation }
        |> Modal conf


modalOpenState : Modal msg -> ModalOpenState
modalOpenState (Modal _ state) =
    state.openState


isClosed : Modal msg -> Bool
isClosed (Modal _ state) =
    case state.openState of
        Closed ->
            True

        _ ->
            False


view : Modal msg -> Element msg -> Element msg
view (Modal config state) element =
    let
        renderModal opacity =
            el
                [ width fill
                , height fill
                , onEventNoBubbleEl "click" config.startClosingMsg
                , htmlAttribute <| class "modal-background"
                , Background.color <| rgba255 0 0 0 (0.3 * opacity)
                ]
            <|
                el
                    [ centerX
                    , centerY
                    , width <|
                        minimum 600 <|
                            shrink
                    , height <|
                        minimum 100 <|
                            shrink
                    , Background.color <| rgb255 255 255 255
                    , Border.shadow
                        { offset = ( 0, 0 )
                        , size = 0
                        , blur = 4
                        , color = rgb255 0 0 0
                        }
                    , Border.rounded 10
                    , alpha opacity
                    , scale opacity
                    , config.updateMsg Ignore
                        |> onEventNoBubbleEl "click"
                    ]
                    element
    in
    case state.openState of
        Closed ->
            none

        Opening ->
            state.openAnimation
                |> currentPosition
                |> Maybe.withDefault 1
                |> renderModal

        Closing ->
            1
                - (state.closeAnimation
                    |> currentPosition
                    |> Maybe.withDefault 1
                  )
                |> renderModal

        Open ->
            renderModal 1
