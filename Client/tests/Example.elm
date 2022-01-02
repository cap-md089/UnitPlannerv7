module Example exposing (..)

import ConnectionTest exposing (..)
import Expect exposing (Expectation)
import Fuzz exposing (Fuzzer, int, list, string)
import Html exposing (button, div, span, text)
import Html.Attributes exposing (style)
import Html.Events exposing (onClick)
import Test exposing (..)


type TestMsg
    = SendMsg


suite : Test
suite =
    let
        goodButton =
            div
                [ style "border" "1px solid red"
                , style "padding" "10px"
                ]
                [ button [ onClick SendMsg ] [ text "Test connection" ]
                , span [] [ text "good!" ]
                ]
    in
    describe "Main module"
        [ test "makes a proper button for testing" <|
            \_ ->
                testButton SendMsg (Just True)
                    |> Expect.equal goodButton
        ]
