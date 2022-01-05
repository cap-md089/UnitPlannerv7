-- Animation.elm: Allows for the management and application of animations
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

module Components.Animation exposing (Animation, AnimationMsg, EasingFunction, animationSubscription, createAnimation, currentPosition, easeInCubic, easeOutCubic, easeOutSin, main, startAnimation, updateAnimation, t_TEST_createAnimationMsg)

import Browser
import Browser.Events as E
import Html exposing (Html, button, div, p, text)
import Html.Attributes exposing (style)
import Html.Events exposing (onClick)



{-
   Goes from 0 to 1, and expects the easing function to do the same. If it goes outside that range, it can be used for bounces
-}


type alias EasingFunction =
    Float -> Float


type Animation
    = Animation
        { easingFunction : EasingFunction
        , targetDuration : Float
        , currentPosition : Float
        , running : Bool
        }


createAnimation : EasingFunction -> Float -> Animation
createAnimation easing duration =
    Animation
        { easingFunction = easing
        , targetDuration = duration
        , currentPosition = 0
        , running = False
        }


type AnimationMsg
    = Step Float


t_TEST_createAnimationMsg : Float -> AnimationMsg
t_TEST_createAnimationMsg = Step


animationSubscription : Animation -> Sub AnimationMsg
animationSubscription (Animation animation) =
    if animation.currentPosition >= 1 then
        Sub.none

    else if animation.running then
        E.onAnimationFrameDelta Step

    else
        Sub.none


updateAnimation :
    { handleAnimationProgress : Animation -> model
    , handleAnimationFinish : Animation -> model
    }
    -> AnimationMsg
    -> Animation
    -> model
updateAnimation { handleAnimationProgress, handleAnimationFinish } (Step delta) (Animation animation) =
    if not animation.running then
        handleAnimationProgress <| Animation animation

    else if animation.currentPosition > 1 then
        handleAnimationFinish <| Animation { animation | currentPosition = 1, running = False }

    else
        let
            newProgress =
                animation.currentPosition + delta / animation.targetDuration

            newAnimation =
                { animation | currentPosition = newProgress }
        in
        if newProgress >= 1 then
            handleAnimationFinish <| Animation { newAnimation | currentPosition = 1, running = False }

        else
            handleAnimationProgress <| Animation newAnimation


currentPosition : Animation -> Maybe Float
currentPosition (Animation animation) =
    if animation.running then
        animation.easingFunction animation.currentPosition
            |> Just

    else
        Nothing


startAnimation : Animation -> Animation
startAnimation (Animation animation) =
    Animation { animation | running = True, currentPosition = 0 }


easeOutSin : EasingFunction
easeOutSin pos =
    sin ((pos * pi) / 2)


easeOutCubic : EasingFunction
easeOutCubic pos =
    1 - (1 - pos) ^ 3


easeInCubic : EasingFunction
easeInCubic pos =
    pos ^ 3


type TestMsg
    = StartAnimation
    | AnimationUpdate AnimationMsg


type alias TestModel =
    { test : Animation
    , displayText : Bool
    }


testInit : () -> ( TestModel, Cmd TestMsg )
testInit () =
    ( { displayText = False
      , test = createAnimation easeOutCubic 750
      }
    , Cmd.none
    )


testSubscriptions : TestModel -> Sub TestMsg
testSubscriptions model =
    animationSubscription model.test |> Sub.map AnimationUpdate


testUpdate : TestMsg -> TestModel -> ( TestModel, Cmd TestMsg )
testUpdate msg model =
    case msg of
        StartAnimation ->
            ( { model | test = startAnimation model.test, displayText = False }, Cmd.none )

        AnimationUpdate updateMsg ->
            let
                ifDone newTest =
                    { model | test = newTest, displayText = True }

                ifNotDone newTest =
                    { model | test = newTest }

                newModel =
                    updateAnimation { handleAnimationProgress = ifNotDone, handleAnimationFinish = ifDone } updateMsg model.test
            in
            ( newModel, Cmd.none )


testView : TestModel -> Html TestMsg
testView model =
    let
        width =
            currentPosition model.test
                |> Maybe.map ((*) 100)
                |> Maybe.map String.fromFloat
                |> Maybe.map (\s -> s ++ "%")
                |> Maybe.withDefault "100%"

        textElem =
            if model.displayText then
                [ p [] [ text "Hello" ] ]

            else
                [ p [] [ text "hi" ] ]
    in
    div
        [ style "width" width
        , style "display" "flex"
        , style "justify-content" "center"
        ]
        (button [ onClick StartAnimation ] [ text "start animation" ] :: textElem)


main : Program () TestModel TestMsg
main =
    Browser.element
        { init = testInit
        , view = testView
        , update = testUpdate
        , subscriptions = testSubscriptions
        }
