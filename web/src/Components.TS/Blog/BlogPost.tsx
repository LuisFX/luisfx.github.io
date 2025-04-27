import React, { useState, useEffect } from 'react';
import { BlogPost as BlogPostType, getPostBySlug, generateTableOfContents } from './mdxUtils';
import MarkdownComponents from './MDXComponents';
import ReactMarkdown from 'react-markdown';
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter';
import { atomDark } from 'react-syntax-highlighter/dist/esm/styles/prism';
import remarkGfm from 'remark-gfm';

interface BlogPostProps {
  slug: string;
  onBack: () => void;
}

const BlogPostComponent: React.FC<BlogPostProps> = ({ slug, onBack }) => {
  const [post, setPost] = useState<BlogPostType | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [tableOfContents, setTableOfContents] = useState<{ text: string; id: string }[]>([]);
  
  useEffect(() => {
    async function loadPost() {
      try {
        setIsLoading(true);
        
        // Get post by slug (now async)
        const postData = await getPostBySlug(slug);
        
        if (!postData) {
          setError(`Blog post '${slug}' not found`);
          return;
        }
        
        setPost(postData);
        
        // Generate table of contents if content is available
        if (postData.content) {
          // Remove frontmatter section from content before generating TOC
          const contentWithoutFrontmatter = postData.content.replace(/^---[\s\S]*?---\s*/m, '');
          const toc = generateTableOfContents(contentWithoutFrontmatter);
          setTableOfContents(toc);
        }
        
        setError(null);
      } catch (err) {
        console.error('Error loading post:', err);
        setError('Failed to load blog post');
      } finally {
        setIsLoading(false);
      }
    }
    
    loadPost();
  }, [slug]);
  
  // Format date for display
  const formatDate = (dateString: string): string => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { 
      year: 'numeric', 
      month: 'long', 
      day: 'numeric' 
    });
  };
  
  // Calculate reading time
  const calculateReadingTime = (content: string): number => {
    const wordsPerMinute = 200;
    const words = content.trim().split(/\s+/).length;
    return Math.ceil(words / wordsPerMinute);
  };
  
  if (isLoading) {
    return (
      <div className="flex justify-center items-center min-h-[50vh]">
        <div className="loading loading-spinner text-primary loading-lg"></div>
      </div>
    );
  }
  
  if (error || !post) {
    return (
      <div className="container mx-auto px-4 py-8 max-w-4xl text-center">
        <div className="alert alert-error">
          <span>{error || 'Blog post not found'}</span>
        </div>
        <button 
          className="btn btn-primary mt-4"
          onClick={onBack}
        >
          Back to Blog
        </button>
      </div>
    );
  }
  
  // If content is missing, show an error
  if (!post.content) {
    return (
      <div className="container mx-auto px-4 py-8 max-w-4xl text-center">
        <div className="alert alert-error">
          <span>Failed to load content for "{post.frontmatter.title}"</span>
        </div>
        <button 
          className="btn btn-primary mt-4"
          onClick={onBack}
        >
          Back to Blog
        </button>
      </div>
    );
  }
  
  // Remove frontmatter section from content for display
  const contentWithoutFrontmatter = post.content.replace(/^---[\s\S]*?---\s*/m, '');
  const readingTime = calculateReadingTime(contentWithoutFrontmatter);
  
  return (
    <div className="container mx-auto px-4 py-12 max-w-7xl">
      <button 
        className="btn btn-ghost btn-sm mb-8 gap-2"
        onClick={onBack}
      >
        <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10 19l-7-7m0 0l7-7m-7 7h18" />
        </svg>
        Back to all posts
      </button>
      
      <div className="flex flex-col md:flex-row gap-8">
        {/* Main content */}
        <div className="w-full md:w-3/4">
          {post.frontmatter.coverImage && (
            <img 
              src={post.frontmatter.coverImage} 
              alt={post.frontmatter.title} 
              className="w-full h-96 object-cover rounded-lg mb-8 shadow-lg"
            />
          )}
          
          <h1 className="text-4xl md:text-5xl lg:text-6xl font-black mb-4">
            {post.frontmatter.title}
          </h1>
          
          <div className="flex flex-wrap items-center gap-4 mb-8">
            <div className="flex items-center">
              <div className="avatar">
                <div className="w-10 h-10 rounded-full bg-primary flex items-center justify-center text-primary-content font-bold">
                  {post.frontmatter.author.charAt(0)}
                </div>
              </div>
              <span className="ml-2">{post.frontmatter.author}</span>
            </div>
            
            <span className="text-sm opacity-70">•</span>
            
            <div className="flex items-center">
              <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 mr-1 opacity-70" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
              </svg>
              <span>{formatDate(post.frontmatter.date)}</span>
            </div>
            
            <span className="text-sm opacity-70">•</span>
            
            <div className="flex items-center">
              <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 mr-1 opacity-70" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
              </svg>
              <span>{readingTime} min read</span>
            </div>
          </div>
          
          <div className="flex flex-wrap gap-2 mb-8">
            {post.frontmatter.tags.map((tag, index) => (
              <span key={index} className="badge badge-primary">
                {tag}
              </span>
            ))}
          </div>
          
          <div className="prose prose-lg max-w-none dark:prose-invert">
            <ReactMarkdown
              remarkPlugins={[remarkGfm]}
              components={MarkdownComponents}
            >
              {contentWithoutFrontmatter}
            </ReactMarkdown>
          </div>
        </div>
        
        {/* Sidebar with table of contents */}
        <div className="hidden md:block md:w-1/4">
          <div className="sticky top-20">
            <div className="bg-base-200 rounded-xl p-6">
              <h3 className="text-xl font-bold mb-4">Table of Contents</h3>
              
              {tableOfContents.length > 0 ? (
                <ul className="space-y-2">
                  {tableOfContents.map((item, index) => (
                    <li key={index}>
                      <a 
                        href={`#${item.id}`}
                        className="hover:text-primary transition-colors"
                      >
                        {item.text}
                      </a>
                    </li>
                  ))}
                </ul>
              ) : (
                <p className="text-sm opacity-70">No table of contents available</p>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BlogPostComponent; 