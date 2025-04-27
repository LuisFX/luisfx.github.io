namespace App.Pages

module Blog =
    open Feliz
    open App
    open System
    open Browser.Dom

    // Types for blog post data
    type BlogPostSummary = {
        Slug: string
        Title: string
        Date: DateTime
        Author: string
        Tags: string list
        Excerpt: string
        CoverImage: string option
    }

    type BlogPostContent = {
        Metadata: BlogPostSummary
        Content: string // MDX content as string
        TableOfContents: (string * string) list // (heading, id) pairs
    }

    // State management for Blog page
    type BlogState = 
        | Loading
        | LoadError of string
        | LoadedList of BlogPostSummary list
        | LoadedPost of BlogPostContent

    // Mock data for blog posts
    let mockBlogPosts = [
        {
            Slug = "getting-started-with-fsharp"
            Title = "Getting Started with F# and Fable"
            Date = DateTime(2023, 12, 15)
            Author = "Luis FX"
            Tags = ["F#"; "Fable"; "Web Development"]
            Excerpt = "Learn how to set up your first F# web project with Fable and create interactive web applications with a functional approach."
            CoverImage = Some "https://via.placeholder.com/800x400?text=F%23+%26+Fable"
        }
        {
            Slug = "functional-react-patterns"
            Title = "Functional React Patterns with Feliz"
            Date = DateTime(2024, 2, 10)
            Author = "Luis FX"
            Tags = ["F#"; "React"; "Feliz"; "Patterns"]
            Excerpt = "Discover how to leverage functional programming patterns in your React applications using Feliz."
            CoverImage = Some "https://via.placeholder.com/800x400?text=Functional+React+Patterns"
        }
        {
            Slug = "tailwind-best-practices"
            Title = "Tailwind CSS Best Practices"
            Date = DateTime(2024, 4, 1)
            Author = "Luis FX"
            Tags = ["CSS"; "Tailwind"; "Design"]
            Excerpt = "Learn the best practices for scaling and maintaining Tailwind CSS in large applications."
            CoverImage = None
        }
    ]
    
    // Find a blog post by slug
    let findBlogPostBySlug (slug: string) =
        mockBlogPosts 
        |> List.tryFind (fun p -> p.Slug = slug) 
        |> Option.map (fun metadata ->
            // Mock MDX content
            let content = """
# ${metadata.Title}

Published on *${metadata.Date.ToShortDateString()}* by **${metadata.Author}**

${metadata.Excerpt}

## Introduction

This is where the introduction would go.

## Main Content

This is the main content of the blog post.

### Subsection

This is a subsection with some code:

```fsharp
let add a b = a + b
```

## Conclusion

This is the conclusion of the blog post.
"""
            
            // Mock table of contents
            let tableOfContents = [
                "Introduction", "introduction"
                "Main Content", "main-content"
                "Subsection", "subsection"
                "Conclusion", "conclusion"
            ]
            
            {
                Metadata = metadata
                Content = content
                TableOfContents = tableOfContents
            }
        )
        
    // Display a blog post card in the list view
    let renderBlogCard (post: BlogPostSummary) (onReadMore: string -> unit) =
        Html.div [
            prop.className "card bg-base-200 shadow-xl hover:shadow-2xl transition-all duration-300 h-full"
            prop.children [
                match post.CoverImage with
                | Some imgUrl ->
                    Html.figure [
                        prop.className "px-6 pt-6"
                        prop.children [
                            Html.img [
                                prop.src imgUrl
                                prop.alt post.Title
                                prop.className "rounded-xl h-48 w-full object-cover"
                            ]
                        ]
                    ]
                | None -> ()
                
                Html.div [
                    prop.className "card-body"
                    prop.children [
                        Html.h2 [
                            prop.className "card-title text-2xl font-bold text-primary"
                            prop.text post.Title
                        ]
                        
                        Html.div [
                            prop.className "text-sm opacity-70 mb-2"
                            prop.children [
                                Html.span [
                                    prop.text (post.Date.ToString("MMMM dd, yyyy"))
                                ]
                                Html.span [
                                    prop.text " • "
                                ]
                                Html.span [
                                    prop.text post.Author
                                ]
                            ]
                        ]
                        
                        Html.p [
                            prop.className "mb-4 line-clamp-3"
                            prop.text post.Excerpt
                        ]
                        
                        Html.div [
                            prop.className "flex flex-wrap gap-2 mb-4"
                            prop.children [
                                for tag in post.Tags do
                                    Html.span [
                                        prop.className "badge badge-outline badge-sm"
                                        prop.text tag
                                    ]
                            ]
                        ]
                        
                        Html.div [
                            prop.className "card-actions justify-end"
                            prop.children [
                                Html.button [
                                    prop.className "btn btn-primary btn-sm"
                                    prop.text "Read more"
                                    prop.onClick (fun _ -> onReadMore post.Slug)
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    
    // Display a single blog post
    let renderBlogPost (post: BlogPostContent) (onBack: unit -> unit) =
        Html.div [
            prop.className "container mx-auto px-4 py-8 max-w-4xl"
            prop.children [
                Html.div [
                    prop.className "flex flex-col md:flex-row gap-8"
                    prop.children [
                        // Main content
                        Html.div [
                            prop.className "flex-grow"
                            prop.children [
                                Html.div [
                                    prop.className "mb-8"
                                    prop.children [
                                        Html.button [
                                            prop.className "btn btn-ghost btn-sm"
                                            prop.onClick (fun _ -> onBack())
                                            prop.children [
                                                Html.i [
                                                    prop.className "fa-solid fa-arrow-left mr-2"
                                                ]
                                                Html.span "Back to all posts"
                                            ]
                                        ]
                                    ]
                                ]
                                
                                match post.Metadata.CoverImage with
                                | Some imgUrl ->
                                    Html.img [
                                        prop.src imgUrl
                                        prop.alt post.Metadata.Title
                                        prop.className "w-full rounded-xl mb-8 max-h-80 object-cover"
                                    ]
                                | None -> ()
                                
                                Html.h1 [
                                    prop.className "text-4xl md:text-5xl font-black mb-4"
                                    prop.text post.Metadata.Title
                                ]
                                
                                Html.div [
                                    prop.className "flex items-center gap-4 mb-8 opacity-70"
                                    prop.children [
                                        Html.div [
                                            prop.children [
                                                Html.span [
                                                    prop.text (post.Metadata.Date.ToString("MMMM dd, yyyy"))
                                                ]
                                                Html.span [
                                                    prop.text " • "
                                                ]
                                                Html.span [
                                                    prop.text post.Metadata.Author
                                                ]
                                            ]
                                        ]
                                    ]
                                ]
                                
                                Html.div [
                                    prop.className "flex flex-wrap gap-2 mb-8"
                                    prop.children [
                                        for tag in post.Metadata.Tags do
                                            Html.span [
                                                prop.className "badge badge-outline"
                                                prop.text tag
                                            ]
                                    ]
                                ]
                                
                                // Here we would render the MDX content
                                // For now, just display the content as plain text
                                Html.div [
                                    prop.className "prose prose-lg max-w-none"
                                    prop.dangerouslySetInnerHTML post.Content
                                ]
                            ]
                        ]
                        
                        // Table of contents (only on desktop)
                        Html.div [
                            prop.className "hidden md:block w-64 shrink-0"
                            prop.children [
                                Html.div [
                                    prop.className "sticky top-20"
                                    prop.children [
                                        Html.div [
                                            prop.className "bg-base-200 rounded-xl p-6"
                                            prop.children [
                                                Html.h3 [
                                                    prop.className "text-xl font-bold mb-4"
                                                    prop.text "Table of Contents"
                                                ]
                                                
                                                Html.ul [
                                                    prop.className "space-y-2"
                                                    prop.children [
                                                        for (heading, id) in post.TableOfContents do
                                                            Html.li [
                                                                Html.a [
                                                                    prop.href ("#" + id)
                                                                    prop.className "hover:text-primary transition-colors"
                                                                    prop.text heading
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
        ]
    
    // Display the blog list view
    let renderBlogList (posts: BlogPostSummary list) (onReadMore: string -> unit) =
        Html.div [
            prop.className "container mx-auto px-4 py-8"
            prop.children [
                Html.h1 [
                    prop.className "text-4xl md:text-5xl font-black mb-12 text-center"
                    prop.text "Blog"
                ]
                
                Html.div [
                    prop.className "grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8"
                    prop.children [
                        for post in posts do
                            renderBlogCard post onReadMore
                    ]
                ]
            ]
        ]
    
    // Main blog component
    [<ReactComponent>]
    let Page (dispatch: App.State.Msg -> unit) =
        let (blogState, setBlogState) = React.useState(LoadedList mockBlogPosts)
        
        // Event handlers
        let showPostDetails slug =
            match findBlogPostBySlug slug with
            | Some post -> setBlogState (LoadedPost post)
            | None -> setBlogState (LoadError $"Blog post '{slug}' not found")
                
        let showBlogList () =
            setBlogState (LoadedList mockBlogPosts)
            
        let tryAgain () =
            setBlogState (LoadedList mockBlogPosts)
        
        Html.div [
            prop.className "min-h-screen bg-base-100"
            prop.children [
                match blogState with
                | Loading ->
                    Html.div [
                        prop.className "flex justify-center items-center min-h-[50vh]"
                        prop.children [
                            Html.div [
                                prop.className "loading loading-spinner text-primary loading-lg"
                            ]
                        ]
                    ]
                
                | LoadError error ->
                    Html.div [
                        prop.className "container mx-auto px-4 py-8 flex flex-col items-center justify-center min-h-[50vh]"
                        prop.children [
                            Html.div [
                                prop.className "alert alert-error max-w-xl"
                                prop.children [
                                    Html.i [
                                        prop.className "fa-solid fa-circle-exclamation text-xl"
                                    ]
                                    Html.span [
                                        prop.text (sprintf "Error loading blog content: %s" error)
                                    ]
                                ]
                            ]
                            Html.button [
                                prop.className "btn btn-primary mt-4"
                                prop.text "Try Again"
                                prop.onClick (fun _ -> tryAgain())
                            ]
                        ]
                    ]
                
                | LoadedList posts ->
                    renderBlogList posts showPostDetails
                
                | LoadedPost post ->
                    renderBlogPost post showBlogList
            ]
        ]
        
    // Additional sample blog posts and page component
    type SimpleBlogPost = {
        Title: string
        Date: string
        Summary: string
        Content: string
    }

    let sampleBlogPosts = [
        { 
            Title = "Getting Started with F#"
            Date = "May 15, 2023"
            Summary = "An introduction to F# programming language and its key features."
            Content = "F# is a functional-first programming language that makes it easy to write correct and maintainable code. In this post, we'll explore the basics of F# and how it can be used to build robust applications."
        }
        { 
            Title = "Building Web Apps with SAFE Stack"
            Date = "June 10, 2023"
            Summary = "Learn how to create full-stack web applications using the SAFE Stack."
            Content = "The SAFE Stack is a free, open-source, flexible web development stack for building web applications using F#. It stands for Saturn, Azure, Fable, and Elmish, which are the main components of this stack."
        }
    ]

    let SimpleBlogPage = React.functionComponent(fun () -> 
        Html.div [
            prop.className "container"
            prop.children [
                Html.h1 [
                    prop.className "title is-2"
                    prop.text "Blog"
                ]
                Html.p [
                    prop.className "subtitle"
                    prop.text "Welcome to my blog! Here are some of my latest posts."
                ]
                
                Html.div [
                    prop.className "columns is-multiline"
                    prop.children [
                        for post in sampleBlogPosts do
                            Html.div [
                                prop.className "column is-half"
                                prop.children [
                                    Html.div [
                                        prop.className "card"
                                        prop.children [
                                            Html.div [
                                                prop.className "card-content"
                                                prop.children [
                                                    Html.h4 [
                                                        prop.className "title is-4"
                                                        prop.text post.Title
                                                    ]
                                                    Html.h6 [
                                                        prop.className "subtitle is-6"
                                                        prop.text post.Date
                                                    ]
                                                    Html.p [
                                                        prop.text post.Summary
                                                    ]
                                                ]
                                            ]
                                            Html.div [
                                                prop.className "card-footer"
                                                prop.children [
                                                    Html.a [
                                                        prop.className "card-footer-item"
                                                        prop.text "Read more"
                                                        prop.href "#"
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
    ) 