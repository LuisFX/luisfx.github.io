// ts2fable 0.9.0
module rec Blog
open System
open Feliz
open Fable.Core.JsInterop
open Fable.Core.JS
open Fable.Core
open Fable.React

// Define property types for the Blog component
type BlogProps =
    | InitialSlug of string

// Create the F# binding for the Blog component
[<ReactComponent>]
let Blog (props: BlogProps list) =
    let propsObj = 
        props
        |> List.fold (fun acc prop ->
            match prop with
            | InitialSlug slug -> acc?initialSlug <- slug
            acc) (createObj [])
    
    Feliz.Interop.reactApi.createElement(importDefault "./Blog.tsx", propsObj)

// Default export without props
[<ReactComponent>]
let BlogDefault () =
    Feliz.Interop.reactApi.createElement(importDefault "./Blog.tsx", {||}) 