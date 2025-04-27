// ts2fable 0.9.0
module rec Test
open System
open Feliz
open Fable.Core.JsInterop
open Fable.Core.JS
open Fable.Core
open Fable.React

[<ReactComponent>]
let Test () =
    Feliz.Interop.reactApi.createElement (importDefault "./Test.tsx", {||})
