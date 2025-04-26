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
            | About -> Pages.About.Page dispatch
            | Projects -> Pages.Projects.Page dispatch
            | Contact -> Pages.Contact.Page dispatch
            | Experience -> Html.text "Experience page coming soon"
            | Skills -> Html.text "Skills page coming soon"
            | Blog -> Html.text "Blog page coming soon"
                
        // Handle initial URL
        React.useEffectOnce(fun () -> 
            let initialPage = 
                Router.currentUrl()
                |> State.fromUrl 

            dispatch (NavigateTo initialPage)
            React.createDisposable(fun () -> ())
        )
        
        // Main component
        Layouts.MainLayout model dispatch (renderPage model dispatch)
        
    [<ReactComponent>]
    let App () = Router() 