namespace App.Pages

open Feliz
open App

module Home =
    [<ReactComponent>]
    let Page (dispatch: App.State.Msg -> unit) =
        Html.div [
            prop.className "min-h-screen"
            prop.children [
                // Hero section
                Html.section [
                    prop.className "hero min-h-screen bg-gradient-animated"
                    prop.children [
                        Html.div [
                            prop.className "hero-content text-center text-neutral-content"
                            prop.children [
                                Html.div [
                                    prop.className "max-w-md"
                                    prop.children [
                                        Html.h1 [
                                            prop.className "mb-5 text-5xl font-bold"
                                            prop.children [
                                                Html.text "Luis"
                                                Html.span [
                                                    prop.className "text-accent"
                                                    prop.text "FX"
                                                ]
                                            ]
                                        ]
                                        Html.p [
                                            prop.className "mb-5 text-xl"
                                            prop.text "CTO at AntidoteAI | Polyglot Developer | AI Enthusiast"
                                        ]
                                        Html.p [
                                            prop.className "mb-5 animate-float"
                                            prop.text "Building tomorrow's tech with today's tools while sailing through the digital ocean 🌊💻"
                                        ]
                                        Html.div [
                                            prop.className "flex flex-wrap justify-center gap-2 mt-8"
                                            prop.children [
                                                Html.button [
                                                    prop.className "btn btn-primary"
                                                    prop.onClick (fun _ -> dispatch (App.State.NavigateTo Projects))
                                                    prop.text "View Projects"
                                                ]
                                                Html.button [
                                                    prop.className "btn btn-outline btn-accent"
                                                    prop.onClick (fun _ -> dispatch (App.State.NavigateTo Contact))
                                                    prop.text "Contact Me"
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