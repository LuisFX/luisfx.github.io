namespace App.Pages

open Feliz
open App

module Projects =
    type ProjectCategory = 
        | All
        | WebDevelopment
        | ArtificialIntelligence
        | MobileDevelopment
        | CloudInfrastructure
    
    type Project = {
        Title: string
        Description: string
        Category: ProjectCategory
        Tags: string list
        Year: int
        ImageUrl: string option
    }
    
    let projects = [
        {
            Title = "AI-ENABLED EMR SYSTEM"
            Description = "Advanced Electronic Medical Record system with AI-powered diagnosis assistance and workflow optimization."
            Category = ArtificialIntelligence
            Tags = ["F#"; "ML.NET"; "Azure"; "React"; "PostgreSQL"]
            Year = 2024
            ImageUrl = None
        }
        {
            Title = "DISTRIBUTED SUPPLY CHAIN PLATFORM"
            Description = "End-to-end supply chain management platform for the apparel industry with real-time tracking and analytics."
            Category = CloudInfrastructure
            Tags = ["F#"; "Kubernetes"; "Microservices"; "GraphQL"; "Event Sourcing"]
            Year = 2023
            ImageUrl = None
        }
        {
            Title = "MULTI-MODEL LLM FRAMEWORK"
            Description = "Framework for orchestrating and optimizing multiple LLMs for complex AI tasks with context management."
            Category = ArtificialIntelligence
            Tags = ["Python"; "F#"; "TensorFlow"; "ONNX"; "Docker"]
            Year = 2023
            ImageUrl = None
        }
        {
            Title = "MOBILE HEALTH ANALYTICS"
            Description = "Cross-platform mobile application for health analytics with offline capabilities and synchronization."
            Category = MobileDevelopment
            Tags = ["Kotlin"; "Swift"; "Xamarin"; "SQLite"; "Bluetooth LE"]
            Year = 2022
            ImageUrl = None
        }
        {
            Title = "REAL-TIME COLLABORATION PLATFORM"
            Description = "Web-based real-time collaboration tool with document editing, video conferencing, and project management."
            Category = WebDevelopment
            Tags = ["TypeScript"; "React"; "WebRTC"; "Socket.IO"; "MongoDB"]
            Year = 2022
            ImageUrl = None
        }
        {
            Title = "SERVERLESS DATA PIPELINE"
            Description = "Serverless data processing pipeline for high-volume analytics with auto-scaling capabilities."
            Category = CloudInfrastructure
            Tags = ["F#"; "AWS Lambda"; "Kinesis"; "DynamoDB"; "Terraform"]
            Year = 2021
            ImageUrl = None
        }
    ]
    
    let getCategoryName = function
        | All -> "ALL"
        | WebDevelopment -> "WEB"
        | ArtificialIntelligence -> "AI"
        | MobileDevelopment -> "MOBILE"
        | CloudInfrastructure -> "CLOUD"
    
    let getCategoryColor = function
        | All -> "primary"
        | WebDevelopment -> "info"
        | ArtificialIntelligence -> "accent"
        | MobileDevelopment -> "secondary"
        | CloudInfrastructure -> "success"
    
    let projectCard (project: Project) (dispatch: App.State.Msg -> unit) =
        Html.div [
            prop.className "bg-base-200 rounded-xl p-6 card-hover min-h-[300px] flex flex-col"
            prop.children [
                Html.div [
                    prop.className "mb-4"
                    prop.children [
                        Html.span [
                            prop.className $"inline-block px-3 py-1 text-xs font-medium bg-{getCategoryColor project.Category}/20 text-{getCategoryColor project.Category} rounded-full"
                            prop.text (getCategoryName project.Category)
                        ]
                    ]
                ]
                
                Html.h3 [
                    prop.className "text-2xl font-bold mb-3"
                    prop.text project.Title
                ]
                
                Html.p [
                    prop.className "text-base-content/80 mb-6 flex-grow"
                    prop.text project.Description
                ]
                
                Html.div [
                    prop.className "flex flex-wrap gap-2 mb-4"
                    prop.children [
                        for tag in project.Tags do
                            Html.span [
                                prop.className "px-2 py-1 bg-base-300 text-xs font-mono rounded"
                                prop.text tag
                            ]
                    ]
                ]
                
                Html.div [
                    prop.className "flex justify-between items-center mt-auto pt-4 border-t border-base-300"
                    prop.children [
                        Html.span [
                            prop.className "text-sm opacity-70"
                            prop.text $"{project.Year}"
                        ]
                        
                        Html.button [
                            prop.className "btn btn-sm btn-ghost"
                            prop.text "VIEW DETAILS"
                        ]
                    ]
                ]
            ]
        ]
        
    let categoryButton (category: ProjectCategory) (selectedCategory: ProjectCategory) (setSelectedCategory: ProjectCategory -> unit) =
        let isSelected = category = selectedCategory
        let baseClasses = "btn"
        let activeClasses = 
            if isSelected then 
                $"btn-{getCategoryColor category}" 
            else 
                "btn-ghost"
        
        Html.button [
            prop.className $"{baseClasses} {activeClasses}"
            prop.onClick (fun _ -> setSelectedCategory category)
            prop.text (getCategoryName category)
        ]

    [<ReactComponent>]
    let Page (dispatch: App.State.Msg -> unit) =
        let (selectedCategory, setSelectedCategory) = React.useState(All)
        
        let filteredProjects = 
            match selectedCategory with
            | All -> projects
            | category -> projects |> List.filter (fun p -> p.Category = category)
        
        Html.div [
            prop.className "min-h-screen"
            prop.children [
                // Header
                Html.section [
                    prop.className "py-20 bg-gradient-animated text-white"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.h1 [
                                    prop.className "text-7xl md:text-9xl font-black tracking-tight mb-6"
                                    prop.text "PROJECTS"
                                ]
                                
                                Html.p [
                                    prop.className "text-xl md:text-2xl max-w-2xl"
                                    prop.text "A selection of my work across various domains and technologies."
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Category filter
                Html.section [
                    prop.className "py-8 bg-base-200 sticky top-16 z-10 shadow-md"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.div [
                                    prop.className "flex flex-wrap gap-3 justify-center md:justify-start"
                                    prop.children [
                                        categoryButton All selectedCategory setSelectedCategory
                                        categoryButton WebDevelopment selectedCategory setSelectedCategory
                                        categoryButton ArtificialIntelligence selectedCategory setSelectedCategory
                                        categoryButton MobileDevelopment selectedCategory setSelectedCategory
                                        categoryButton CloudInfrastructure selectedCategory setSelectedCategory
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Projects grid
                Html.section [
                    prop.className "py-16 bg-base-100"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8"
                            prop.children [
                                Html.div [
                                    prop.className "grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8"
                                    prop.children [
                                        for project in filteredProjects do
                                            projectCard project dispatch
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                
                // Contact CTA
                Html.section [
                    prop.className "py-20 bg-base-200"
                    prop.children [
                        Html.div [
                            prop.className "container mx-auto px-4 md:px-8 text-center"
                            prop.children [
                                Html.h2 [
                                    prop.className "text-3xl md:text-5xl font-bold mb-6"
                                    prop.text "INTERESTED IN WORKING TOGETHER?"
                                ]
                                
                                Html.p [
                                    prop.className "text-xl opacity-80 mb-8 max-w-2xl mx-auto"
                                    prop.text "Let's discuss your project ideas and see how we can collaborate."
                                ]
                                
                                Html.button [
                                    prop.className "btn btn-lg btn-primary"
                                    prop.onClick (fun _ -> dispatch (App.State.NavigateTo Contact))
                                    prop.text "GET IN TOUCH"
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ] 