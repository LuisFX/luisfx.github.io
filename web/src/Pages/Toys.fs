module Pages.Toys

open Feliz
open Feliz.DaisyUI
open App
open Fable.Core.JsInterop
open TowerBlocks

[<ReactComponent>]
let Toys () =
    let (activeGame, setActiveGame) = React.useState<string option>(None)
    
    Html.div [
        prop.className "min-h-screen p-6 md:p-8 lg:p-12 bg-gradient-to-br from-primary/10 to-secondary/5"
        prop.children [
            Html.div [
                prop.className "text-center mb-12 transition-all duration-500 ease-in-out"
                prop.children [
                    Html.h1 [
                        prop.className "text-4xl md:text-5xl lg:text-6xl font-bold text-primary mb-4 tracking-tight"
                        prop.text "Interactive Toys"
                    ]
                    Html.p [
                        prop.className "text-lg md:text-xl text-base-content/70 max-w-2xl mx-auto"
                        prop.text "Explore these fun interactive experiments that showcase web technologies and creative coding."
                    ]
                ]
            ]
            
            match activeGame with
            | Some "TowerBlocks" -> 
                Html.div [
                    prop.className "w-full mx-auto bg-base-200 rounded-xl shadow-lg overflow-hidden transition-all duration-500 ease-in-out"
                    prop.style [
                        style.height 80
                        style.maxWidth 900
                        style.position.relative
                        style.custom("height", "80vh")
                        style.custom("maxWidth", "900px")
                    ]
                    prop.children [
                        Html.div [
                            prop.className "absolute top-4 right-4 z-50"
                            prop.children [
                                Html.button [
                                    prop.className "btn btn-circle btn-sm bg-white/30 hover:bg-white/50 backdrop-blur-sm"
                                    prop.onClick (fun _ -> setActiveGame None)
                                    prop.children [
                                        Html.i [
                                            prop.className "fa-solid fa-times"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                        Html.div [
                            prop.className "w-full h-full"
                            prop.style [
                                style.custom("height", "100%")
                            ]
                            prop.children [
                                TowerBlocks()
                            ]
                        ]
                    ]
                ]
            | _ ->
                Html.div [
                    prop.className "grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 max-w-6xl mx-auto"
                    prop.children [
                        // TowerBlocks game card
                        Html.div [
                            prop.className "bg-base-100 rounded-xl shadow-md overflow-hidden hover:shadow-lg transition-all duration-300 ease-in-out transform hover:-translate-y-1"
                            prop.children [
                                Html.div [
                                    prop.className "h-48 bg-gradient-to-br from-primary to-secondary flex items-center justify-center"
                                    prop.children [
                                        Html.i [
                                            prop.className "fa-solid fa-cubes text-6xl text-white opacity-80"
                                        ]
                                    ]
                                ]
                                Html.div [
                                    prop.className "p-5"
                                    prop.children [
                                        Html.h3 [
                                            prop.className "text-xl font-bold mb-2"
                                            prop.text "Tower Blocks"
                                        ]
                                        Html.p [
                                            prop.className "text-base-content/70 mb-4"
                                            prop.text "Stack the blocks as high as you can in this challenging 3D stacking game."
                                        ]
                                        Html.button [
                                            prop.className "btn btn-primary w-full"
                                            prop.onClick (fun _ -> setActiveGame (Some "TowerBlocks"))
                                            prop.text "Play Now"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                        
                        // Placeholder for future games
                        Html.div [
                            prop.className "bg-base-100 rounded-xl shadow-md overflow-hidden opacity-50 hover:opacity-60 transition-all duration-300"
                            prop.children [
                                Html.div [
                                    prop.className "h-48 bg-gradient-to-br from-neutral to-neutral-focus flex items-center justify-center"
                                    prop.children [
                                        Html.i [
                                            prop.className "fa-solid fa-gamepad text-6xl text-white opacity-50"
                                        ]
                                    ]
                                ]
                                Html.div [
                                    prop.className "p-5"
                                    prop.children [
                                        Html.h3 [
                                            prop.className "text-xl font-bold mb-2"
                                            prop.text "Coming Soon"
                                        ]
                                        Html.p [
                                            prop.className "text-base-content/70 mb-4"
                                            prop.text "More interactive toys will be added soon. Stay tuned!"
                                        ]
                                        Html.button [
                                            prop.className "btn btn-disabled w-full"
                                            prop.text "Coming Soon"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
        ]
    ]

[<ReactComponent>]
let Page dispatch =
    Toys()