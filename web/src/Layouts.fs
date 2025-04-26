namespace App

open Feliz
open Feliz.Router
open Browser.Dom
open App.State

module Layouts =
    [<ReactComponent>]
    let NavLink (page: Page) (currentPage: Page) (label: string) =
        let isActive = page = currentPage
        let baseClasses = "nav-link"
        let activeClasses = if isActive then "nav-link-active" else baseClasses
        
        Html.a [
            prop.className activeClasses
            prop.href (Router.format(State.toUrl page))
            prop.onClick (fun e -> 
                e.preventDefault()
                // Router.navigate(Page.toUrl page)
            )
            prop.text label
        ]
    
    [<ReactComponent>]
    let Header (model: Model) (dispatch: Msg -> unit) =
        let isScrolled = model.ScrollPosition > 10.0
        let headerClasses = 
            if isScrolled then 
                "navbar sticky top-0 z-50 bg-base-100/90 backdrop-blur-sm shadow-md transition-all duration-300"
            else 
                "navbar sticky top-0 z-50 bg-base-100 transition-all duration-300"
        
        Html.header [
            prop.className headerClasses
            prop.children [
                Html.div [
                    prop.className "navbar-start"
                    prop.children [
                        Html.button [
                            prop.className "btn btn-ghost lg:hidden"
                            prop.onClick (fun _ -> dispatch ToggleMenu)
                            prop.children [
                                Html.i [
                                    prop.className "fa-solid fa-bars"
                                ]
                            ]
                        ]
                        Html.a [
                            prop.className "btn btn-ghost normal-case text-xl"
                            prop.href (Router.format([]))
                            prop.onClick (fun e -> 
                                e.preventDefault()
                                dispatch (NavigateTo Home))
                            prop.children [
                                Html.span [
                                    prop.className "font-bold"
                                    prop.text "Luis"
                                ]
                                Html.span [
                                    prop.className "text-primary"
                                    prop.text "FX"
                                ]
                            ]
                        ]
                    ]
                ]
                Html.div [
                    prop.className "navbar-center hidden lg:flex"
                    prop.children [
                        Html.ul [
                            prop.className "menu menu-horizontal px-1 gap-1"
                            prop.children [
                                Html.li [
                                    NavLink Home model.CurrentPage "Home"
                                ]
                                Html.li [
                                    NavLink About model.CurrentPage "About"
                                ]
                                Html.li [
                                    NavLink Experience model.CurrentPage "Experience"
                                ]
                                Html.li [
                                    NavLink Projects model.CurrentPage "Projects"
                                ]
                                Html.li [
                                    NavLink Skills model.CurrentPage "Skills"
                                ]
                                Html.li [
                                    NavLink Blog model.CurrentPage "Blog"
                                ]
                                Html.li [
                                    NavLink Contact model.CurrentPage "Contact"
                                ]
                            ]
                        ]
                    ]
                ]
                Html.div [
                    prop.className "navbar-end"
                    prop.children [
                        Theme.ThemeToggle(model.CurrentTheme, fun theme -> dispatch (SetTheme theme))
                    ]
                ]
            ]
        ]
        
    [<ReactComponent>]
    let MobileMenu (model: Model) (dispatch: Msg -> unit) =
        if model.IsMenuOpen then
            Html.div [
                prop.className "fixed inset-0 z-40 bg-black/50 backdrop-blur-sm lg:hidden"
                prop.onClick (fun _ -> dispatch ToggleMenu)
                prop.children [
                    Html.div [
                        prop.className "fixed inset-y-0 left-0 w-64 bg-base-100 shadow-xl p-4 transform transition-transform"
                        prop.onClick (fun e -> e.stopPropagation())
                        prop.children [
                            Html.div [
                                prop.className "flex justify-between items-center mb-6"
                                prop.children [
                                    Html.span [
                                        prop.className "text-2xl font-bold"
                                        prop.children [
                                            Html.span [ prop.text "Luis" ]
                                            Html.span [ 
                                                prop.className "text-primary"
                                                prop.text "FX" 
                                            ]
                                        ]
                                    ]
                                    Html.button [
                                        prop.className "btn btn-ghost btn-sm"
                                        prop.onClick (fun _ -> dispatch ToggleMenu)
                                        prop.children [
                                            Html.i [ prop.className "fa-solid fa-times" ]
                                        ]
                                    ]
                                ]
                            ]
                            Html.ul [
                                prop.className "menu menu-vertical w-full gap-1"
                                prop.children [
                                    Html.li [
                                        NavLink Home model.CurrentPage "Home"
                                    ]
                                    Html.li [
                                        NavLink About model.CurrentPage "About"
                                    ]
                                    Html.li [
                                        NavLink Experience model.CurrentPage "Experience"
                                    ]
                                    Html.li [
                                        NavLink Projects model.CurrentPage "Projects"
                                    ]
                                    Html.li [
                                        NavLink Skills model.CurrentPage "Skills"
                                    ]
                                    Html.li [
                                        NavLink Blog model.CurrentPage "Blog"
                                    ]
                                    Html.li [
                                        NavLink Contact model.CurrentPage "Contact"
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        else
            Html.none
            
    [<ReactComponent>]
    let Footer () =
        Html.footer [
            prop.className "footer footer-center p-10 bg-base-200 text-base-content"
            prop.children [
                Html.div [
                    prop.children [
                        Html.div [
                            prop.className "grid grid-flow-col gap-4"
                            prop.children [
                                Html.a [
                                    prop.href "#"
                                    prop.className "link link-hover"
                                    prop.text "LinkedIn"
                                ]
                                Html.a [
                                    prop.href "#"
                                    prop.className "link link-hover"
                                    prop.text "GitHub"
                                ]
                                Html.a [
                                    prop.href "#"
                                    prop.className "link link-hover"
                                    prop.text "Twitter"
                                ]
                            ]
                        ]
                    ]
                ]
                Html.div [
                    prop.children [
                        Html.p [
                            prop.text "© 2025 Luis FX - Built with "
                            // Html.a [
                            //     prop.className "link link-primary"
                            //     prop.href "https://fable.io"
                            //     prop.target "_blank"
                            //     prop.text "Fable"
                            // ]
                            // Html.text " and "
                            // Html.a [
                            //     prop.className "link link-primary" 
                            //     prop.href "https://feliz-tools.github.io"
                            //     prop.target "_blank"
                            //     prop.text "Feliz"
                            // ]
                        ]
                    ]
                ]
            ]
        ]
        
    [<ReactComponent>]
    let MainLayout (model: Model) (dispatch: Msg -> unit) (content: ReactElement) =
        // Add scroll event listener
        React.useEffect(
            fun () ->
                let handleScroll _ = 
                    dispatch (UpdateScrollPosition window.scrollY)
                
                window.addEventListener("scroll", handleScroll)
                
                // Cleanup
                React.createDisposable(fun () -> 
                    window.removeEventListener("scroll", handleScroll)
                ) |> ignore
                ()
        , [||]
        )
        
        Html.div [
            prop.className "min-h-screen flex flex-col"
            prop.children [
                Header model dispatch
                MobileMenu model dispatch
                Html.main [
                    prop.className "flex-grow"
                    prop.children [ content ]
                ]
                Footer()
            ]
        ] 