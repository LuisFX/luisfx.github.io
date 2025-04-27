// ts2fable 0.9.0
module rec SampleMDX
open System
open Feliz
open Fable.Core.JsInterop
open Fable.Core.JS
open Fable.Core
open Fable.React

[<ReactComponent>]
let SampleMDX () =
    // Feliz.Interop.reactApi.createElement (importDefault "./Sample.mdx", {||})
    Html.span "SampleMDX"
