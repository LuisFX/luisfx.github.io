module rec TowerBlocks
open System
open Feliz
open Fable.Core.JsInterop
open Fable.Core.JS
open Fable.Core
open Fable.React

[<ReactComponent>]
let TowerBlocks () =
    Feliz.Interop.reactApi.createElement (importDefault "./TowerBlocks.tsx", {||}) 