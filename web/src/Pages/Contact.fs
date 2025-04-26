namespace App.Pages

open Feliz
open App

module Contact =
    [<ReactComponent>]
    let Page (dispatch: App.State.Msg -> unit) =
        let (name, setName) = React.useState("")
        let (email, setEmail) = React.useState("")
        let (message, setMessage) = React.useState("")
        let (isSubmitted, setIsSubmitted) = React.useState(false)
        
        let handleSubmit (e: Browser.Types.Event) =
            e.preventDefault()
            // In a real application, this would send the form data to a server
            setIsSubmitted(true)
        
        Html.div [
            prop.className "min-h-screen"
            prop.children [
                // Header section
                Html.section [
                    prop.className "py-20 bg-gradient-animated text-white"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.h1 [
                                    prop.className "text-7xl md:text-9xl font-black tracking-tight mb-6"
                                    prop.text "CONTACT"
                                ]
                                
                                Html.p [
                                    prop.className "text-xl md:text-2xl max-w-2xl"
                                    prop.text "Let's discuss your next project or just have a conversation about technology."
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Contact form and info
                Html.section [
                    prop.className "py-20 bg-base-100"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.div [
                                    prop.className "grid grid-cols-1 md:grid-cols-2 gap-16"
                                    prop.children [
                                        // Contact form
                                        Html.div [
                                            if isSubmitted then
                                                Html.div [
                                                    prop.className "bg-success/10 text-success p-8 rounded-xl"
                                                    prop.children [
                                                        Html.h3 [
                                                            prop.className "text-2xl font-bold mb-4"
                                                            prop.text "MESSAGE SENT"
                                                        ]
                                                        
                                                        Html.p [
                                                            prop.className "mb-6"
                                                            prop.text "Thank you for your message! I'll get back to you as soon as possible."
                                                        ]
                                                        
                                                        Html.button [
                                                            prop.className "btn btn-outline btn-success"
                                                            prop.onClick (fun _ -> setIsSubmitted(false))
                                                            prop.text "SEND ANOTHER MESSAGE"
                                                        ]
                                                    ]
                                                ]
                                            else
                                                Html.form [
                                                    prop.onSubmit handleSubmit
                                                    prop.children [
                                                        Html.h2 [
                                                            prop.className "text-3xl font-bold mb-8"
                                                            prop.text "GET IN TOUCH"
                                                        ]
                                                        
                                                        Html.div [
                                                            prop.className "form-control mb-6"
                                                            prop.children [
                                                                Html.label [
                                                                    prop.className "label"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "label-text text-lg"
                                                                            prop.text "NAME"
                                                                        ]
                                                                    ]
                                                                ]
                                                                
                                                                Html.input [
                                                                    prop.className "input input-bordered w-full"
                                                                    prop.type' "text"
                                                                    prop.required true
                                                                    prop.value name
                                                                    prop.onChange setName
                                                                ]
                                                            ]
                                                        ]
                                                        
                                                        Html.div [
                                                            prop.className "form-control mb-6"
                                                            prop.children [
                                                                Html.label [
                                                                    prop.className "label"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "label-text text-lg"
                                                                            prop.text "EMAIL"
                                                                        ]
                                                                    ]
                                                                ]
                                                                
                                                                Html.input [
                                                                    prop.className "input input-bordered w-full"
                                                                    prop.type' "email"
                                                                    prop.required true
                                                                    prop.value email
                                                                    prop.onChange setEmail
                                                                ]
                                                            ]
                                                        ]
                                                        
                                                        Html.div [
                                                            prop.className "form-control mb-8"
                                                            prop.children [
                                                                Html.label [
                                                                    prop.className "label"
                                                                    prop.children [
                                                                        Html.span [
                                                                            prop.className "label-text text-lg"
                                                                            prop.text "MESSAGE"
                                                                        ]
                                                                    ]
                                                                ]
                                                                
                                                                Html.textarea [
                                                                    prop.className "textarea textarea-bordered w-full min-h-[150px]"
                                                                    prop.required true
                                                                    prop.value message
                                                                    prop.onChange setMessage
                                                                ]
                                                            ]
                                                        ]
                                                        
                                                        Html.button [
                                                            prop.className "btn btn-primary btn-lg w-full"
                                                            prop.type' "submit"
                                                            prop.text "SEND MESSAGE"
                                                        ]
                                                    ]
                                                ]
                                        ]
                                        
                                        // Contact information
                                        Html.div [
                                            prop.className "space-y-12"
                                            prop.children [
                                                Html.div [
                                                    prop.className "mb-12"
                                                    prop.children [
                                                        Html.h2 [
                                                            prop.className "text-3xl font-bold mb-6"
                                                            prop.text "CONNECT WITH ME"
                                                        ]
                                                        
                                                        Html.p [
                                                            prop.className "text-lg opacity-80 mb-8"
                                                            prop.text "Feel free to reach out through any of these channels. I'm always open to discussing new projects, creative ideas, or opportunities to be part of your visions."
                                                        ]
                                                    ]
                                                ]
                                                
                                                Html.div [
                                                    prop.className "space-y-4"
                                                    prop.children [
                                                        // contactMethod "EMAIL" "info@luisfx.dev" "mail"
                                                        // contactMethod "LINKEDIN" "/in/luisfx" "linkedin"
                                                        // contactMethod "GITHUB" "@luisfx" "github"
                                                        // contactMethod "TWITTER" "@luisfxdev" "twitter"
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
                
                // Call to action
                Html.section [
                    prop.className "py-20 bg-base-200"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8 text-center"
                            prop.children [
                                Html.div [
                                    prop.className "max-w-3xl mx-auto"
                                    prop.children [
                                        Html.h2 [
                                            prop.className "text-5xl font-bold mb-8"
                                            prop.text "LET'S BUILD SOMETHING AMAZING"
                                        ]
                                        
                                        Html.p [
                                            prop.className "text-xl opacity-80 mb-12"
                                            prop.text "Whether you have a specific project in mind or just want to explore possibilities, I'm here to help turn ideas into reality."
                                        ]
                                        
                                        Html.div [
                                            prop.className "flex flex-wrap justify-center gap-4"
                                            prop.children [
                                                Html.button [
                                                    prop.className "btn btn-primary btn-lg"
                                                    prop.onClick (fun _ -> dispatch (App.State.NavigateTo Projects))
                                                    prop.text "VIEW MY WORK"
                                                ]
                                                
                                                Html.button [
                                                    prop.className "btn btn-outline btn-lg"
                                                    prop.onClick (fun _ -> dispatch (App.State.NavigateTo About))
                                                    prop.text "LEARN MORE ABOUT ME"
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
    
    let contactMethod (label: string) (value: string) (icon: string) =
        Html.div [
            prop.className "flex items-start space-x-4 p-4 bg-base-200 rounded-lg"
            prop.children [
                Html.div [
                    prop.className "bg-primary/10 p-2 rounded-full"
                    prop.children [
                        Html.span [
                            prop.className "text-primary text-xl"
                            prop.text "●" // Placeholder for icon
                        ]
                    ]
                ]
                
                Html.div [
                    prop.children [
                        Html.div [
                            prop.className "font-bold"
                            prop.text label
                        ]
                        
                        Html.div [
                            prop.text value
                        ]
                    ]
                ]
            ]
        ] 