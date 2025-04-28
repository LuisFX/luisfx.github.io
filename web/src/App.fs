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
           ScrollPosition = 0
           BlogSlug = None }, Cmd.none)

    let update (msg: Msg) (model: Model) =
        match msg with
        | SetTheme theme -> ({ model with CurrentTheme = theme }, Cmd.none)
        | NavigateTo page -> ({ model with CurrentPage = page; BlogSlug = None }, Cmd.none)
        | NavigateToBlogPost slug -> ({ model with CurrentPage = BlogPost; BlogSlug = Some slug }, Cmd.none)
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
            | Blog -> 
                Blog.BlogDefault()
            | BlogPost ->
                match model.BlogSlug with
                | Some slug -> Blog.Blog [ Blog.BlogProps.InitialSlug slug ]
                | None -> Blog.BlogDefault()
            | Toys -> Pages.Toys.Toys()

        // Create a router with a URL handler
        React.router [
            router.onUrlChanged (fun segments ->
                match segments with
                | [ "blog"; slug ] -> NavigateToBlogPost slug |> dispatch
                | _ -> State.fromUrl segments |> NavigateTo |> dispatch
            )
            router.children [
                Layouts.MainLayout model dispatch (renderPage model dispatch)
            ]
        ]
        
    [<ReactComponent>]
    let App () =
        Router()
