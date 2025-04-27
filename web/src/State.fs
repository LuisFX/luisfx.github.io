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
    | BlogPost
    | Toys
    | Contact

module State =

    let fromUrl = function
        | [ ] -> Home
        | [ "about" ] -> About
        | [ "experience" ] -> Experience
        | [ "projects" ] -> Projects
        | [ "skills" ] -> Skills
        | [ "blog" ] -> Blog
        | [ "toys" ] -> Toys
        | [ "contact" ] -> Contact
        | _ -> Home

    let toUrl = function
        | Home -> [ ]
        | About -> [ "about" ]
        | Experience -> [ "experience" ]
        | Projects -> [ "projects" ]
        | Skills -> [ "skills" ]
        | Blog -> [ "blog" ]
        | BlogPost -> [ "blog" ]
        | Toys -> [ "toys" ]
        | Contact -> [ "contact" ]
        
    let toTitle = function
        | Home -> "Home"
        | About -> "About"
        | Experience -> "Experience"
        | Projects -> "Projects" 
        | Skills -> "Skills"
        | Blog -> "Blog"
        | BlogPost -> "Blog Post"
        | Toys -> "Toys"
        | Contact -> "Contact"

    type Model = {
        CurrentPage: Page
        CurrentTheme: Theme.ThemeType
        IsMenuOpen: bool
        ScrollPosition: float
        BlogSlug: string option
    }

    type Msg =
        | NavigateTo of Page
        | NavigateToBlogPost of string
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
            BlogSlug = None
        }, Cmd.none

    let update (msg: Msg) (model: Model) =
        match msg with
        | NavigateTo page ->
            window.scrollTo(0.0, 0.0)
            { model with 
                CurrentPage = page 
                IsMenuOpen = false
                BlogSlug = None
            }, Cmd.none
            
        | NavigateToBlogPost slug ->
            window.scrollTo(0.0, 0.0)
            { model with 
                CurrentPage = BlogPost
                IsMenuOpen = false
                BlogSlug = Some slug
            }, Cmd.none
            
        | SetTheme theme ->
            Theme.setTheme theme
            { model with CurrentTheme = theme }, Cmd.none
            
        | ToggleMenu ->
            { model with IsMenuOpen = not model.IsMenuOpen }, Cmd.none
            
        | UpdateScrollPosition position ->
            { model with ScrollPosition = position }, Cmd.none 