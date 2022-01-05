-- AnimationTest.elm: Tests animations and the state management involved
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

module Components.Animation.AnimationTest exposing (..)

import Expect
import Test exposing (..)
import Components.Animation exposing (..)

suite : Test
suite =
    let
        identityHandlers =
            { handleAnimationFinish = \anim -> anim
            , handleAnimationProgress = \anim -> anim
            }
    in
    describe "Animations"
        [ test "animation should not send the wrong update" <|
            \_ ->
                let
                    handlers =
                        { handleAnimationFinish = \_ -> False
                        , handleAnimationProgress = \_ -> True
                        }
                in
                createAnimation easeOutCubic 1
                    |> updateAnimation handlers (t_TEST_createAnimationMsg 0)
                    |> Expect.equal True
        , test "animation should send a finish update when done" <|
            \_ ->
                let
                    handlers =
                        { handleAnimationFinish = \_ -> True
                        , handleAnimationProgress = \_ -> False
                        }
                in
                createAnimation easeOutCubic 1
                    |> startAnimation
                    |> updateAnimation handlers (t_TEST_createAnimationMsg 1)
                    |> Expect.equal True
        , test "animations should start at 0" <|
            \_ ->
                createAnimation easeOutCubic 1
                    |> startAnimation
                    |> currentPosition
                    |> Expect.equal (Just 0)
        , test "animations should finish close to 1" <|
            \_ ->
                createAnimation easeOutCubic 1
                    |> startAnimation
                    |> updateAnimation identityHandlers (t_TEST_createAnimationMsg 0.999)
                    |> currentPosition
                    |> Maybe.withDefault 0
                    |> Expect.greaterThan 0.95
        , test "animations should finish with nothing" <|
            \_ ->
                createAnimation easeOutCubic 1
                    |> updateAnimation identityHandlers (t_TEST_createAnimationMsg 100)
                    |> currentPosition
                    |> Expect.equal (Nothing)
        ]