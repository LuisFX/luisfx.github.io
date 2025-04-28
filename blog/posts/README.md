# Blog Posts Directory

This directory contains MDX files for blog posts that will be loaded at runtime by the blog system.

## Structure

Each blog post is an MDX file with frontmatter followed by markdown content.

### Example Frontmatter

```md
---
title: "Getting Started with F# and Fable"
date: "2023-12-15"
author: "Luis FX"
tags: ["F#", "Fable", "Web Development"]
excerpt: "Learn how to set up your first F# web project with Fable and create interactive web applications with a functional approach."
coverImage: "/images/blog/fsharp-fable.jpg"
---
```

## Registration

For a post to appear in the blog, it must be registered in the `blogpost.manifest.json` file with the correct slug and filename.

## Runtime Loading

Posts are dynamically loaded at runtime using fetch to retrieve the MDX content, which is then processed and rendered with ReactMarkdown. 