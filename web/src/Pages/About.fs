namespace App.Pages

open Feliz
open App

module About =
    let languageTile (name: string) (colorClass: string) (proficiency: int) =
        Html.div [
            // prop.className ($"p-4 rounded-lg bg-{colorClass}/10 hover:bg-{colorClass}/20 transition-all duration-300 cursor-pointer border-2 border-{colorClass}/20")
            prop.children [
                Html.div [
                    prop.className "text-center"
                    prop.children [
                        Html.span [
                            prop.className (sprintf "text-%s font-bold text-xl" colorClass)
                            prop.text name
                        ]
                        
                        Html.div [
                            prop.className "mt-2 h-1 bg-base-300 rounded-full overflow-hidden"
                            prop.children [
                                Html.div [
                                    // prop.className $"h-full bg-{colorClass}"
                                    prop.style [
                                        style.width (length.percent proficiency)
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    
    let interestTag (text: string) (emoji: string) =
        Html.div [
            prop.className "px-4 py-2 bg-base-300 rounded-full flex items-center gap-2"
            prop.children [
                Html.span [
                    prop.text emoji
                ]
                Html.span [
                    prop.text text
                ]
            ]
        ]
    
    let techTag (name: string) =
        Html.span [
            prop.className "px-3 py-1 bg-base-300 text-base-content rounded-md text-sm font-mono"
            prop.text name
        ]

    [<ReactComponent>]
    let Page (dispatch: App.State.Msg -> unit) =
        Html.div [
            prop.className "min-h-screen"
            prop.children [
                // Main about section with horizontal scroll
                Html.section [
                    prop.className "py-20 bg-base-100"
                    prop.children [
                        Html.div [
                            prop.className "section-container"
                            prop.children [
                                Html.h1 [
                                    prop.className "text-7xl md:text-9xl font-black tracking-tight mb-16 opacity-90"
                                    prop.text "ABOUT"
                                ]
                                
                                Html.div [
                                    prop.className "grid grid-cols-1 md:grid-cols-2 gap-12"
                                    prop.children [
                                        Html.div [
                                            prop.className "space-y-6"
                                            prop.children [
                                                Html.h2 [
                                                    prop.className "text-4xl font-bold"
                                                    prop.text "CTO & POLYGLOT ARCHITECT"
                                                ]
                                                
                                                Html.p [
                                                    prop.className "text-xl opacity-80 leading-relaxed"
                                                    prop.text "I'm a tech-obsessed software engineer who loves creating solutions that make a real difference. When I'm not crafting code, you'll find me sailing the open waters or hunting down the perfect bowl of ramen!"
                                                ]
                                                
                                                Html.div [
                                                    prop.className "pt-6"
                                                    prop.children [
                                                        Html.h3 [
                                                            prop.className "text-2xl font-bold mb-4"
                                                            prop.text "WHAT I BRING"
                                                        ]
                                                        
                                                        Html.ul [
                                                            prop.className "space-y-3"
                                                            prop.children [
                                                                Html.li [
                                                                    prop.className "flex items-start"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "text-primary mr-2"
                                                                            prop.text "▹"
                                                                        ]
                                                                        Html.span "Full Stack Development expert"
                                                                    ]
                                                                ]
                                                                Html.li [
                                                                    prop.className "flex items-start"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "text-primary mr-2"
                                                                            prop.text "▹"
                                                                        ]
                                                                        Html.span "AI Development & LLM Training specialist"
                                                                    ]
                                                                ]
                                                                Html.li [
                                                                    prop.className "flex items-start"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "text-primary mr-2"
                                                                            prop.text "▹"
                                                                        ]
                                                                        Html.span "Cloud Development & SAAS Architecture virtuoso"
                                                                    ]
                                                                ]
                                                                Html.li [
                                                                    prop.className "flex items-start"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "text-primary mr-2"
                                                                            prop.text "▹"
                                                                        ]
                                                                        Html.span "Multi-paradigm Programming aficionado"
                                                                    ]
                                                                ]
                                                            ]
                                                        ]
                                                    ]
                                                ]
                                            ]
                                        ]
                                        
                                        // Languages section with visual representation
                                        Html.div [
                                            prop.className "bg-base-200 p-8 rounded-xl"
                                            prop.children [
                                                Html.h3 [
                                                    prop.className "text-2xl font-bold mb-6"
                                                    prop.text "LANGUAGES I SPEAK"
                                                ]
                                                
                                                Html.div [
                                                    prop.className "grid grid-cols-3 gap-4"
                                                    prop.children [
                                                        // Language tiles
                                                        languageTile "F#" "primary" 90
                                                        languageTile "Swift" "secondary" 85
                                                        languageTile "C#" "accent" 95
                                                        languageTile "Kotlin" "info" 80
                                                        languageTile "Python" "success" 85
                                                        languageTile "JavaScript" "warning" 90
                                                    ]
                                                ]
                                                
                                                Html.div [
                                                    prop.className "mt-8"
                                                    prop.children [
                                                        Html.h3 [
                                                            prop.className "text-2xl font-bold mb-4"
                                                            prop.text "WHEN I'M NOT CODING"
                                                        ]
                                                        
                                                        Html.div [
                                                            prop.className "flex flex-wrap gap-3"
                                                            prop.children [
                                                                interestTag "Sailing" "🚤"
                                                                interestTag "Coding" "💻"
                                                                interestTag "Traveling" "✈️"
                                                                interestTag "Culinary Adventures" "🍜"
                                                                interestTag "Fishing" "🎣"
                                                            ]
                                                        ]
                                                    ]
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Featured project section
                Html.section [
                    prop.className "py-20 bg-base-200"
                    prop.children [
                        Html.div [
                            prop.className "section-container"
                            prop.children [
                                Html.div [
                                    prop.className "text-center mb-12"
                                    prop.children [
                                        Html.h2 [
                                            prop.className "text-5xl md:text-7xl font-black tracking-tight"
                                            prop.text "HIGHLIGHT PROJECT"
                                        ]
                                    ]
                                ]
                                
                                Html.div [
                                    prop.className "bg-base-100 p-8 rounded-xl shadow-lg"
                                    prop.children [
                                        Html.div [
                                            prop.className "flex flex-col md:flex-row gap-8"
                                            prop.children [
                                                Html.div [
                                                    prop.className "flex-1"
                                                    prop.children [
                                                        Html.h3 [
                                                            prop.className "text-3xl font-bold mb-4"
                                                            prop.text "AI-ENABLED EMR SYSTEM"
                                                        ]
                                                        
                                                        Html.p [
                                                            prop.className "text-lg opacity-80 mb-4"
                                                            prop.text "Designed and implemented an advanced AI-enabled Electronic Medical Record (EMR/HMR) system that revolutionized healthcare data management and clinical workflows."
                                                        ]
                                                        
                                                        Html.div [
                                                            prop.className "flex flex-wrap gap-2 mt-4"
                                                            prop.children [
                                                                techTag "F#"
                                                                techTag "ML.NET"
                                                                techTag "Azure"
                                                                techTag "React"
                                                                techTag "PostgreSQL"
                                                            ]
                                                        ]
                                                        
                                                        Html.button [
                                                            prop.className "btn btn-primary mt-8"
                                                            prop.onClick (fun _ -> dispatch (App.State.NavigateTo Projects))
                                                            prop.text "VIEW ALL PROJECTS"
                                                        ]
                                                    ]
                                                ]
                                                
                                                Html.div [
                                                    prop.className "flex-1 flex items-center justify-center"
                                                    prop.children [
                                                        // Placeholder for project image or interactive element
                                                        Html.div [
                                                            prop.className "w-full h-64 bg-gradient-primary rounded-lg flex items-center justify-center text-white text-4xl font-bold"
                                                            prop.children [
                                                                Html.text "EMR"
                                                                Html.span [
                                                                    prop.className "text-accent"
                                                                    prop.text "AI"
                                                                ]
                                                            ]
                                                        ]
                                                    ]
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Philosophy section
                Html.section [
                    prop.className "py-20 bg-gradient-animated text-white"
                    prop.children [
                        Html.div [
                            prop.className "section-container"
                            prop.children [
                                Html.div [
                                    prop.className "text-center max-w-3xl mx-auto"
                                    prop.children [
                                        Html.h2 [
                                            prop.className "text-5xl md:text-7xl font-black tracking-tight mb-12"
                                            prop.text "MY PHILOSOPHY"
                                        ]
                                        
                                        Html.p [
                                            prop.className "text-2xl font-light leading-relaxed"
                                            prop.text "Technology should solve real problems while being accessible to everyone. I believe the best innovations happen when diverse perspectives meet at the intersection of creativity and technical expertise."
                                        ]
                                        
                                        Html.div [
                                            prop.className "mt-12"
                                            prop.children [
                                                Html.button [
                                                    prop.className "btn btn-lg btn-outline btn-accent glass"
                                                    prop.onClick (fun _ -> dispatch (App.State.NavigateTo Contact))
                                                    prop.text "LET'S BUILD SOMETHING AMAZING"
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ] 