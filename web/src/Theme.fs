namespace App

open Feliz
open Browser.Dom
open Browser.WebStorage

module Theme =
    type ThemeType =
        | Light
        | Dark
        | Cyberpunk
        | Synthwave
        | Retro
        
    let fromString = function
        | "light" -> Light
        | "dark" -> Dark
        | "cyberpunk" -> Cyberpunk
        | "synthwave" -> Synthwave
        | "retro" -> Retro
        | _ -> Light
        
    let toString = function
        | Light -> "light"
        | Dark -> "dark"
        | Cyberpunk -> "cyberpunk"
        | Synthwave -> "synthwave"
        | Retro -> "retro"
        
    let themeKey = "luisfx-theme"
    
    let defaultTheme = Light
    
    let getStoredTheme() =
        localStorage.getItem themeKey
        |> Option.ofObj
        |> Option.map fromString
        |> Option.defaultValue defaultTheme
        
    let setTheme (theme: ThemeType) =
        let themeStr = toString theme
        localStorage.setItem(themeKey, themeStr)
        
        // Set the data-theme attribute on the root element
        document.documentElement.setAttribute("data-theme", themeStr)
        
        // Optionally also set a class on the body element for additional theme control
        match theme with
        | Light | Retro -> 
            document.body.classList.remove("dark-theme")
            document.body.classList.add("light-theme")
        | Dark | Cyberpunk | Synthwave -> 
            document.body.classList.remove("light-theme")
            document.body.classList.add("dark-theme")
        
    let isDarkTheme theme =
        match theme with
        | Dark | Cyberpunk | Synthwave -> true
        | _ -> false
        
    let toggleTheme (currentTheme: ThemeType) =
        match currentTheme with
        | Light -> Dark
        | Dark -> Cyberpunk
        | Cyberpunk -> Synthwave
        | Synthwave -> Retro
        | Retro -> Light
    
    let getThemeIcon = function
        | Light -> "☀️"
        | Dark -> "🌙"
        | Cyberpunk -> "🤖"
        | Synthwave -> "🌆"
        | Retro -> "👾"
        
    let getThemeName = function
        | Light -> "Light"
        | Dark -> "Dark"
        | Cyberpunk -> "Cyberpunk"
        | Synthwave -> "Synthwave"
        | Retro -> "Retro"
        
    [<ReactComponent>]
    let ThemeToggle (currentTheme: ThemeType, onThemeChange: ThemeType -> unit) =
        let icon = getThemeIcon currentTheme
        let themeName = getThemeName currentTheme
        
        Html.div [
            prop.className "dropdown dropdown-end"
            prop.children [
                Html.label [
                    prop.className "btn btn-ghost btn-circle swap swap-rotate"
                    prop.htmlFor "theme-dropdown"
                    prop.children [
                        Html.span [
                            prop.className "text-xl"
                            prop.text icon
                        ]
                    ]
                ]
                
                Html.input [
                    prop.id "theme-dropdown"
                    prop.type' "checkbox"
                    prop.className "dropdown-toggle"
                ]
                
                Html.div [
                    prop.className "dropdown-content z-[1] menu p-2 shadow bg-base-200 rounded-box w-52"
                    prop.children [
                        Html.div [
                            prop.className "p-2 text-sm font-semibold text-center"
                            prop.text "Select Theme"
                        ]
                        
                        Html.button [
                            prop.className (sprintf "btn btn-sm btn-block justify-start %s" (if currentTheme = Light then "btn-primary" else "btn-ghost"))
                            prop.onClick (fun _ -> onThemeChange Light)
                            prop.children [
                                Html.span [
                                    prop.className "mr-2"
                                    prop.text "☀️"
                                ]
                                Html.span "Light"
                            ]
                        ]
                        
                        Html.button [
                            prop.className (sprintf "btn btn-sm btn-block justify-start %s" (if currentTheme = Dark then "btn-primary" else "btn-ghost"))
                            prop.onClick (fun _ -> onThemeChange Dark)
                            prop.children [
                                Html.span [
                                    prop.className "mr-2"
                                    prop.text "🌙"
                                ]
                                Html.span "Dark"
                            ]
                        ]
                        
                        Html.button [
                            prop.className (sprintf "btn btn-sm btn-block justify-start %s" (if currentTheme = Cyberpunk then "btn-primary" else "btn-ghost"))
                            prop.onClick (fun _ -> onThemeChange Cyberpunk)
                            prop.children [
                                Html.span [
                                    prop.className "mr-2"
                                    prop.text "🤖"
                                ]
                                Html.span "Cyberpunk"
                            ]
                        ]
                        
                        Html.button [
                            prop.className (sprintf "btn btn-sm btn-block justify-start %s" (if currentTheme = Synthwave then "btn-primary" else "btn-ghost"))
                            prop.onClick (fun _ -> onThemeChange Synthwave)
                            prop.children [
                                Html.span [
                                    prop.className "mr-2"
                                    prop.text "🌆"
                                ]
                                Html.span "Synthwave"
                            ]
                        ]
                        
                        Html.button [
                            prop.className (sprintf "btn btn-sm btn-block justify-start %s" (if currentTheme = Retro then "btn-primary" else "btn-ghost"))
                            prop.onClick (fun _ -> onThemeChange Retro)
                            prop.children [
                                Html.span [
                                    prop.className "mr-2"
                                    prop.text "👾"
                                ]
                                Html.span "Retro"
                            ]
                        ]
                    ]
                ]
            ]
        ]
        
    // Initialize theme from local storage when app starts
    let initializeTheme() =
        let storedTheme = getStoredTheme()
        setTheme storedTheme
        storedTheme 