namespace App.Pages

open Feliz
open App
open Browser.Dom

module Home =
    let skillCategory (title: string) (skills: string list) =
        Html.div [
            prop.className "bg-base-200 rounded-xl p-6 w-80 h-96 flex flex-col relative card-hover"
            prop.children [
                Html.h3 [
                    prop.className "text-2xl font-bold mb-6"
                    prop.text title
                ]
                
                Html.ul [
                    prop.className "space-y-3 flex-grow"
                    prop.children [
                        for skill in skills do
                            Html.li [
                                prop.className "flex items-center"
                                prop.children [
                                    Html.span [
                                        prop.className "text-primary mr-2"
                                        prop.text "▹"
                                    ]
                                    Html.span [
                                        prop.className "text-base-content"
                                        prop.text skill
                                    ]
                                ]
                            ]
                    ]
                ]
                
                Html.div [
                    prop.className "absolute bottom-0 right-0 p-6 opacity-10"
                    prop.children [
                        Html.div [
                            prop.className "text-6xl font-black"
                            prop.text (title.Substring(0, 1))
                        ]
                    ]
                ]
            ]
        ]
        
    let socialButton (text: string) (colorClass: string) =
        Html.button [
            prop.className (sprintf "btn %s btn-lg min-w-[150px]" colorClass)
            prop.text text
        ]

    // Language code fragments with witty titles
    let codeFragments = [
        // F#
        "How about we get F#nctional?",
        "let (|Success|Failure|) result = match result with | Ok x -> Success x | Error e -> Failure e"
        
        // C#
        "C# what I did there?",
        "var results = users.Where(u => u.IsActive).Select(u => u.Name).ToList();"
        
        // TypeScript
        "TS the difference!",
        "const sum = <T extends number[]>(...values: T): number => values.reduce((acc, val) => acc + val, 0);"
        
        // Kotlin
        "Kotlin a lot of features!",
        "val capitalize = { str: String -> str.split(' ').joinToString(\" \") { it.capitalize() } }"
        
        // Swift
        "Be Swift about it!",
        "let sorted = names.sorted { $0.count > $1.count || ($0.count == $1.count && $0 < $1) }"
    ]
    
    [<ReactComponent>]
    let Page (dispatch: App.State.Msg -> unit) =
        Html.div [
            prop.className "min-h-screen"
            prop.children [
                // Hero section
                Html.section [
                    prop.className "min-h-screen bg-gradient-animated flex items-center relative overflow-hidden"
                    prop.children [
                        // Overlay text
                        Html.div [
                            prop.className "absolute inset-0 flex items-center justify-center pointer-events-none"
                            prop.children [
                                Html.div [
                                    prop.className "text-[15vw] font-black tracking-tighter hero-overlay-text"
                                    prop.text "ENGINEERING"
                                ]
                            ]
                        ]
                        
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8 z-10"
                            prop.children [
                                Html.div [
                                    prop.className "grid grid-cols-1 md:grid-cols-2 gap-12 items-center"
                                    prop.children [
                                        Html.div [
                                            prop.className "space-y-6"
                                            prop.children [
                                                Html.h1 [
                                                    prop.className "text-7xl md:text-9xl font-black tracking-tighter text-white"
                                                    prop.children [
                                                        Html.span [
                                                            prop.className "block"
                                                            prop.text "LUIS"
                                                        ]
                                                        Html.span [
                                                            prop.className "text-accent"
                                                            prop.text "FX"
                                                        ]
                                                    ]
                                                ]
                                                
                                                Html.p [
                                                    prop.className "text-xl md:text-2xl text-white/90 max-w-md"
                                                    prop.text "CTO at AntidoteAI | Polyglot Developer | AI Enthusiast"
                                                ]
                                                
                                                Html.p [
                                                    prop.className "text-base md:text-lg text-white/70 max-w-lg animate-float"
                                                    prop.text "Building tomorrow's tech with today's tools while sailing through the digital ocean 🌊💻"
                                                ]
                                                
                                                Html.div [
                                                    prop.className "pt-8 flex gap-4 flex-wrap"
                                                    prop.children [
                                                        Html.button [
                                                            prop.className "btn btn-lg btn-primary"
                                                            prop.onClick (fun _ -> dispatch (App.State.NavigateTo Projects))
                                                            prop.text "VIEW PROJECTS"
                                                        ]
                                                        
                                                        Html.button [
                                                            prop.className "btn btn-lg btn-outline glass text-white hover:text-white"
                                                            prop.onClick (fun _ -> dispatch (App.State.NavigateTo About))
                                                            prop.text "ABOUT ME"
                                                        ]
                                                    ]
                                                ]
                                            ]
                                        ]
                                        
                                        Html.div [
                                            prop.className "hidden md:block"
                                            prop.children [
                                                // Animated code visualization
                                                Html.div [
                                                    prop.className "text-white font-mono text-xs md:text-sm bg-black/20 backdrop-blur-sm p-6 rounded-lg border border-white/10 shadow-xl"
                                                    prop.children [
                                                        let (currentFragmentIndex, setCurrentFragmentIndex) = React.useState(0)
                                                        let (isAnimating, setIsAnimating) = React.useState(true)
                                                        
                                                        let currentTitle, currentCode = codeFragments.[currentFragmentIndex]
                                                        
                                                        // Language title with witty comment
                                                        Html.div [
                                                            prop.className "text-2xl md:text-3xl font-black tracking-tight text-white/90 mb-3"
                                                            prop.text currentTitle
                                                        ]
                                                        
                                                        // Set up the timer to change fragments
                                                        React.useEffect(
                                                            (fun () ->
                                                                let timerId = window.setTimeout(
                                                                    (fun () ->
                                                                        // First remove animation
                                                                        setIsAnimating(false)
                                                                        
                                                                        // Schedule the fragment change
                                                                        window.setTimeout(
                                                                            (fun () ->
                                                                                let nextIndex = (currentFragmentIndex + 1) % codeFragments.Length
                                                                                setCurrentFragmentIndex(nextIndex)
                                                                                
                                                                                // Reset animation after changing text
                                                                                window.setTimeout(
                                                                                    (fun () -> setIsAnimating(true)), 
                                                                                    50
                                                                                ) |> ignore
                                                                            ),
                                                                            100
                                                                        ) |> ignore
                                                                    ),
                                                                    5000
                                                                )
                                                                
                                                                // Cleanup
                                                                React.createDisposable(fun () -> window.clearTimeout(timerId) |> ignore)
                                                            ),
                                                            [| box currentFragmentIndex |]
                                                        )
                                                        
                                                        Html.pre [
                                                            prop.className (
                                                                if isAnimating then 
                                                                    "animate-typing overflow-hidden whitespace-pre" 
                                                                else 
                                                                    "overflow-hidden whitespace-pre"
                                                            )
                                                            prop.text currentCode
                                                        ]
                                                    ]
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                        
                        // Scroll indicator
                        Html.div [
                            prop.className "absolute bottom-8 left-1/2 transform -translate-x-1/2 text-white animate-bounce"
                            prop.children [
                                Html.div [
                                    prop.className "flex flex-col items-center"
                                    prop.children [
                                        Html.span [
                                            prop.className "text-sm opacity-80 mb-2"
                                            prop.text "SCROLL"
                                        ]
                                        Html.div [
                                            prop.className "w-6 h-10 border-2 border-white/30 rounded-full flex justify-center pt-2"
                                            prop.children [
                                                Html.div [
                                                    prop.className "w-1 h-2 bg-white rounded-full animate-pulse"
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Skills section
                Html.section [
                    prop.className "py-20 bg-base-100"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.div [
                                    prop.className "text-center mb-16"
                                    prop.children [
                                        Html.h2 [
                                            prop.className "text-5xl md:text-7xl font-black tracking-tight"
                                            prop.text "SKILLS"
                                        ]
                                    ]
                                ]
                                
                                Html.div [
                                    prop.className "overflow-x-auto pb-8"
                                    prop.children [
                                        Html.div [
                                            prop.className "flex space-x-8 min-w-max"
                                            prop.children [
                                                // Skill categories
                                                skillCategory "LANGUAGES" [
                                                    "F#"; "Swift"; "C#"; "Kotlin"; "Python"; "JavaScript"; "TypeScript"
                                                ]
                                                
                                                skillCategory "FRAMEWORKS" [
                                                    "React"; "Fable.io"; "Vue"; ".NET"; "TensorFlow"; "PyTorch"
                                                ]
                                                
                                                skillCategory "API TECH" [
                                                    "GraphQL"; "REST"; "WebSockets"; "gRPC"
                                                ]
                                                
                                                skillCategory "DATABASES" [
                                                    "SQL"; "MongoDB"; "PostgreSQL"; "Redis"
                                                ]
                                                
                                                skillCategory "CLOUD" [
                                                    "AWS"; "Azure"; "GCP"; "Docker"; "Kubernetes"; "CI/CD"
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                
                // CTA Section
                Html.section [
                    prop.className "py-20 bg-base-200"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.div [
                                    prop.className "text-center"
                                    prop.children [
                                        Html.h2 [
                                            prop.className "text-5xl md:text-7xl font-black tracking-tight mb-8"
                                            prop.text "LET'S CONNECT"
                                        ]
                                        
                                        Html.p [
                                            prop.className "text-2xl opacity-80 mb-12 max-w-2xl mx-auto"
                                            prop.text "Interested in working together? Let's build something amazing that makes a real difference."
                                        ]
                                        
                                        Html.div [
                                            prop.className "flex flex-wrap justify-center gap-4"
                                            prop.children [
                                                socialButton "LINKEDIN" "btn-primary"
                                                socialButton "GITHUB" "btn-secondary" 
                                                socialButton "TWITTER" "btn-accent"
                                                socialButton "EMAIL" "btn-neutral"
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]

                Html.div [
                    prop.className "social-buttons"
                    prop.children [
                        Html.a [
                            prop.className [
                                // Fa.Fa; Fa.FaGithub; Fa.Fa2X
                            ]
                            prop.href "https://github.com/luisfx"
                            prop.target "_blank"
                        ]
                        Html.a [
                            prop.className [
                                // Fa.Fa; Fa.FaLinkedin; Fa.Fa2X
                            ]
                            prop.href "https://www.linkedin.com/in/luisfontouratorres/"
                            prop.target "_blank"
                        ]
                    ]
                ]
            ]
        ]