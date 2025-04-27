namespace App

open Feliz

open Elmish
open Feliz.Router
open Feliz.UseElmish
open Feliz.DaisyUI

module App =
    open App.State
    open App.Pages
    let init () =
        ({ CurrentPage = Home
           CurrentTheme = Theme.Light
           IsMenuOpen = false
           ScrollPosition = 0}, Cmd.none)

    let update (msg: Msg) (model: Model) =
        match msg with
        | SetTheme theme -> ({ model with CurrentTheme = theme }, Cmd.none)
        | NavigateTo page -> ({ model with CurrentPage = page }, Cmd.none)
        | ToggleMenu -> ({ model with IsMenuOpen = not model.IsMenuOpen }, Cmd.none)
        | UpdateScrollPosition position -> ({ model with ScrollPosition = position }, Cmd.none)

    [<ReactComponent>]
    let Router () =
        let model, dispatch = React.useElmish(init, update, [| |])
        
        let renderPage (model: Model) (dispatch: Msg -> unit) =
            match model.CurrentPage with
            | Home -> Pages.Home.Page dispatch
            | About -> Pages.About.Page dispatch
            | Projects -> Pages.Projects.Page dispatch
            | Contact -> Pages.Contact.Page dispatch
            | Experience -> Test.Test()
            | Skills -> SampleMDX.SampleMDX()
            | Blog -> Pages.Blog.Page dispatch
            | Toys -> Pages.Toys.Toys()

        // Create a router with a URL handler
        React.router [
            router.onUrlChanged (State.fromUrl >> NavigateTo >> dispatch)
            router.children [
                Layouts.MainLayout model dispatch (renderPage model dispatch)
            ]
        ]
        
    [<ReactComponent>]
    let App () =
        Router()