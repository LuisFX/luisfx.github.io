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
        let theme = 
            localStorage.getItem themeKey
            |> Option.ofObj
            |> Option.map fromString
            |> Option.defaultValue defaultTheme
        Browser.Dom.console.log("Retrieved theme from storage:", toString theme)
        theme
        
    let setTheme (theme: ThemeType) =
        let themeStr = toString theme
        Browser.Dom.console.log("Setting theme to:", themeStr)
        
        // Save to localStorage
        localStorage.setItem(themeKey, themeStr)
        
        // Set the data-theme attribute on the root element
        document.documentElement.setAttribute("data-theme", themeStr)
        Browser.Dom.console.log("data-theme attribute set to:", document.documentElement.getAttribute("data-theme"))
        
        // Force re-render of styles
        let style = document.createElement("style")
        style.textContent = ":root { }" // Empty style to force a redraw
        document.head.appendChild(style) |> ignore
        
        // Delay for 1ms and then remove the style
        Browser.Dom.window.setTimeout(
            (fun () -> document.head.removeChild(style) |> ignore),
            1
        ) |> ignore
        
        // Optionally also set a class on the body element for additional theme control
        match theme with
        | Light | Retro -> 
            document.body.classList.remove("dark-theme")
            document.body.classList.add("light-theme")
        | Dark | Cyberpunk | Synthwave -> 
            document.body.classList.remove("light-theme")
            document.body.classList.add("dark-theme")
            
        // Force the page to repaint
        Browser.Dom.window.requestAnimationFrame(fun _ -> ()) |> ignore
        
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
        // Track dropdown state
        let (isOpen, setIsOpen) = React.useState(false)
        
        // Log when the component renders
        React.useEffect(fun () -> 
            Browser.Dom.console.log("ThemeToggle component rendered with theme:", toString currentTheme)
            React.createDisposable (fun () -> ())
        , [|box (toString currentTheme)|])
        
        let icon = getThemeIcon currentTheme
        let themeName = getThemeName currentTheme
        
        let handleThemeChange (newTheme: ThemeType) =
            // First close the dropdown
            setIsOpen(false)
            
            // Then change the theme
            Browser.Dom.console.log("Theme button clicked:", toString newTheme)
            
            // Apply theme immediately
            setTheme newTheme
            
            // Then dispatch to update the model
            onThemeChange newTheme
        
        Html.div [
            prop.className "dropdown dropdown-end"
            prop.children [
                Html.button [
                    prop.className "btn btn-ghost btn-circle"
                    prop.ariaLabel "Toggle theme dropdown"
                    prop.onClick (fun _ -> setIsOpen(not isOpen))
                    prop.children [
                        Html.span [
                            prop.className "text-xl"
                            prop.text icon
                        ]
                    ]
                ]
                
                if isOpen then
                    Html.div [
                        prop.className "dropdown-content z-[1] menu p-2 shadow bg-base-200 rounded-box w-52 mt-2"
                        prop.children [
                            Html.div [
                                prop.className "p-2 text-sm font-semibold text-center"
                                prop.text "Select Theme"
                            ]
                            
                            Html.button [
                                prop.className (sprintf "btn btn-sm btn-block justify-start %s" (if currentTheme = Light then "btn-primary" else "btn-ghost"))
                                prop.onClick (fun _ -> handleThemeChange Light)
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
                                prop.onClick (fun _ -> handleThemeChange Dark)
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
                                prop.onClick (fun _ -> handleThemeChange Cyberpunk)
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
                                prop.onClick (fun _ -> handleThemeChange Synthwave)
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
                                prop.onClick (fun _ -> handleThemeChange Retro)
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