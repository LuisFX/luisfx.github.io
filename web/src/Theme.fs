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
        document.documentElement.setAttribute("data-theme", themeStr)
        
    let isDarkTheme theme =
        match theme with
        | Dark | Cyberpunk | Synthwave -> true
        | _ -> false
        
    let toggleTheme (currentTheme: ThemeType) =
        match currentTheme with
        | Light -> Dark
        | Dark -> Light
        | Cyberpunk -> Synthwave
        | Synthwave -> Retro
        | Retro -> Light
        
    let getThemeIcon = function
        | Light -> "☀️"
        | Dark -> "🌙"
        | Cyberpunk -> "🤖"
        | Synthwave -> "🌆"
        | Retro -> "👾"
        
    [<ReactComponent>]
    let ThemeToggle (currentTheme: ThemeType, onThemeChange: ThemeType -> unit) =
        let icon = getThemeIcon currentTheme
        
        Html.button [
            prop.className "btn btn-ghost btn-circle"
            prop.ariaLabel "Toggle theme"
            prop.onClick (fun _ -> 
                let newTheme = toggleTheme currentTheme
                onThemeChange newTheme
            )
            prop.children [
                Html.span [
                    prop.className "text-xl"
                    prop.text icon
                ]
            ]
        ]
        
    // Initialize theme from local storage when app starts
    let initializeTheme() =
        let storedTheme = getStoredTheme()
        setTheme storedTheme
        storedTheme 