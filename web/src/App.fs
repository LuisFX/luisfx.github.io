namespace App

open Feliz

open Elmish
open Feliz.Router
open Feliz.UseElmish

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
            | About -> Html.text "About page coming soon"
            | Experience -> Html.text "Experience page coming soon"
            | Projects -> Html.text "Projects page coming soon"
            | Skills -> Html.text "Skills page coming soon"
            | Blog -> Html.text "Blog page coming soon"
            | Contact -> Html.text "Contact page coming soon"
                
        // Subscribe to URL changes
        React.useEffectOnce(fun () -> 
            let urlSubscription = 
                Router.currentUrl()
                |> App.State.fromUrl
                |> NavigateTo
                |> dispatch
                
            // Router.onUrlChanged (Page.fromUrl >> NavigateTo >> dispatch)
            
            // Cleanup (not really needed in this case since we want the router for the lifetime of the app)
            React.createDisposable(fun () -> ())
        )
        
        // Render
        Layouts.MainLayout model dispatch (renderPage model dispatch)
        
    [<ReactComponent>]
    let App () = Router() 