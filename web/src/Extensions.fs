[<AutoOpen>]
module Extensions

open System
open Fable.Core
open Fable.Core.JsInterop

/// Import side effects from a module without binding the import to a value
[<Emit("import $0")>]
let importSideEffects (path: string): unit = jsNative

[<RequireQualifiedAccess>]
module StaticFile =

    /// Function that imports a static file by it's relative path.
    let inline import (path: string) : string = importDefault<string> path

/// Stylesheet API
/// let private stylesheet = Stylesheet.load "./fancy.module.css"
/// stylesheet.["fancy-class-name"] which returns a string
module Stylesheet =

    type IStylesheet =
        [<Emit "$0[$1]">]
        abstract Item : className:string -> string

    /// Loads a CSS module and makes the classes within available
    let inline load (path: string) = importDefault<IStylesheet> path