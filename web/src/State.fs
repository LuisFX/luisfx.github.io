namespace App

open Elmish
open Browser.Dom

type Page =
    | Home
    | About
    | Experience
    | Projects
    | Skills
    | Blog
    | Contact

module State =

    let fromUrl = function
        | [ ] -> Home
        | [ "about" ] -> About
        | [ "experience" ] -> Experience
        | [ "projects" ] -> Projects
        | [ "skills" ] -> Skills
        | [ "blog" ] -> Blog
        | [ "contact" ] -> Contact
        | _ -> Home

    let toUrl = function
        | Home -> [ ]
        | About -> [ "about" ]
        | Experience -> [ "experience" ]
        | Projects -> [ "projects" ]
        | Skills -> [ "skills" ]
        | Blog -> [ "blog" ]
        | Contact -> [ "contact" ]
        
    let toTitle = function
        | Home -> "Home"
        | About -> "About"
        | Experience -> "Experience"
        | Projects -> "Projects" 
        | Skills -> "Skills"
        | Blog -> "Blog"
        | Contact -> "Contact"

    type Model = {
        CurrentPage: Page
        CurrentTheme: Theme.ThemeType
        IsMenuOpen: bool
        ScrollPosition: float
    }

    type Msg =
        | NavigateTo of Page
        | SetTheme of Theme.ThemeType
        | ToggleMenu
        | UpdateScrollPosition of float

    let init () =
        let initialTheme = Theme.initializeTheme()
        
        {
            CurrentPage = Home
            CurrentTheme = initialTheme
            IsMenuOpen = false
            ScrollPosition = 0.0
        }, Cmd.none

    let update (msg: Msg) (model: Model) =
        match msg with
        | NavigateTo page ->
            window.scrollTo(0.0, 0.0)
            { model with 
                CurrentPage = page 
                IsMenuOpen = false
            }, Cmd.none
            
        | SetTheme theme ->
            Theme.setTheme theme
            { model with CurrentTheme = theme }, Cmd.none
            
        | ToggleMenu ->
            { model with IsMenuOpen = not model.IsMenuOpen }, Cmd.none
            
        | UpdateScrollPosition position ->
            { model with ScrollPosition = position }, Cmd.none 