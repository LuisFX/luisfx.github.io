import React, { useEffect, useState, useRef } from 'react';
import { BlogPost } from './mdxUtils';
import BlogPostSummary from './BlogPostSummary';
import { getAllPosts } from './mdxUtils';

interface BlogIndexProps {
  onPostClick: (slug: string) => void;
}

const BlogIndex: React.FC<BlogIndexProps> = ({ onPostClick }) => {
  const [posts, setPosts] = useState<BlogPost[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [scrollY, setScrollY] = useState(0);
  
  // References for parallax elements
  const parallaxRef = useRef<HTMLDivElement>(null);
  const featuredPostsRef = useRef<HTMLDivElement>(null);
  
  // Handle scroll for parallax effect
  useEffect(() => {
    const handleScroll = () => {
      setScrollY(window.scrollY);
    };
    
    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  useEffect(() => {
    async function loadPosts() {
      try {
        setIsLoading(true);
        const allPosts = await getAllPosts();
        setPosts(allPosts);
        setError(null);
      } catch (err) {
        console.error('Error loading posts:', err);
        setError('Failed to load blog posts. Please try again later.');
      } finally {
        setIsLoading(false);
      }
    }

    loadPosts();
  }, []);

  if (isLoading) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <div className="loading loading-spinner text-primary loading-lg"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="container mx-auto px-4 py-8 max-w-4xl text-center">
        <div className="alert alert-error">
          <span>{error}</span>
        </div>
        <button 
          className="btn btn-primary mt-4"
          onClick={() => window.location.reload()}
        >
          Try Again
        </button>
      </div>
    );
  }

  if (posts.length === 0) {
    return (
      <div className="container mx-auto px-4 py-8 max-w-4xl text-center">
        <h1 className="text-4xl font-bold mb-6">Blog</h1>
        <p className="text-xl">No posts found. Check back soon!</p>
      </div>
    );
  }
  
  // Sort posts to get latest post
  const sortedPosts = [...posts].sort((a, b) => 
    new Date(b.frontmatter.date).getTime() - new Date(a.frontmatter.date).getTime()
  );
  
  const latestPost = sortedPosts[0];
  const remainingPosts = sortedPosts.slice(1);

  return (
    <div className="overflow-hidden bg-base-100">
      {/* Hero Section with Parallax */}
      <div 
        ref={parallaxRef}
        className="relative h-[80vh] flex items-center justify-center overflow-hidden"
        style={{
          background: 'linear-gradient(135deg, #121212 0%, #1a1a1a 100%)'
        }}
      >
        {/* Scrolling Marquee Bar */}
        <div className="absolute top-0 left-0 right-0 bg-primary text-primary-content overflow-hidden py-2 whitespace-nowrap">
          <div className="inline-block animate-marquee">
            FRESH POSTS - LATEST NEWS & ARTICLES - FRESH POSTS - LATEST NEWS & ARTICLES - FRESH POSTS - LATEST NEWS & ARTICLES -
          </div>
          <div className="inline-block animate-marquee2">
            FRESH POSTS - LATEST NEWS & ARTICLES - FRESH POSTS - LATEST NEWS & ARTICLES - FRESH POSTS - LATEST NEWS & ARTICLES -
          </div>
        </div>
        
        {/* Parallax BG Elements */}
        <div 
          className="absolute inset-0 flex items-center justify-center opacity-10"
          style={{
            transform: `translateY(${scrollY * 0.5}px)`,
            transition: 'transform 0.1s linear'
          }}
        >
          <div className="grid grid-cols-3 gap-8 w-full h-full">
            {Array.from({ length: 9 }).map((_, i) => (
              <div 
                key={i} 
                className="rounded-full bg-primary" 
                style={{
                  width: `${Math.random() * 200 + 100}px`,
                  height: `${Math.random() * 200 + 100}px`,
                  transform: `translateX(${Math.random() * 200 - 100}px) translateY(${Math.random() * 200 - 100}px)`,
                  opacity: Math.random() * 0.4 + 0.1
                }}
              />
            ))}
          </div>
        </div>
        
        {/* Large negative title */}
        <div 
          className="relative z-10 container mx-auto px-4"
          style={{
            transform: `translateY(${scrollY * -0.2}px)`,
            transition: 'transform 0.1s linear'
          }}
        >
          <h1 
            className="text-[8rem] md:text-[12rem] lg:text-[16rem] font-black leading-none text-center text-transparent bg-clip-text bg-gradient-to-r from-primary via-primary-focus to-primary"
            style={{ 
              WebkitTextStroke: '2px rgba(255,255,255,0.1)',
              textShadow: '0 10px 30px rgba(0,0,0,0.2)'
            }}
          >
            BLOG
          </h1>
          <p 
            className="text-xl md:text-2xl text-white text-center max-w-2xl mx-auto mt-6 opacity-80"
            style={{
              transform: `translateY(${scrollY * -0.1}px)`,
              transition: 'transform 0.1s linear'
            }}
          >
            Thoughts, stories and ideas about F#, web development, and functional programming.
          </p>
        </div>
        
        {/* Scroll indicator */}
        <div className="absolute bottom-10 left-1/2 transform -translate-x-1/2 animate-bounce">
          <svg 
            xmlns="http://www.w3.org/2000/svg" 
            className="h-8 w-8 text-white opacity-70" 
            fill="none" 
            viewBox="0 0 24 24" 
            stroke="currentColor"
          >
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 14l-7 7m0 0l-7-7m7 7V3" />
          </svg>
        </div>
      </div>
      
      {/* Featured latest post */}
      <div className="bg-base-200 py-16">
        <div className="container mx-auto px-4">
          <div 
            className="mb-12 overflow-hidden"
            ref={featuredPostsRef}
          >
            <h2 
              className="text-5xl md:text-6xl lg:text-7xl font-black mb-6 text-base-content opacity-90"
              style={{ 
                transform: `translateX(${Math.min(0, -(scrollY - 500) * 0.2)}px)`,
                transition: 'transform 0.1s linear'
              }}
            >
              FRESH POSTS
            </h2>
            <div className="h-1 w-32 bg-primary rounded mb-12"></div>
          </div>
          
          {/* Latest post featured card */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-12 items-center mb-24">
            <div 
              className="rounded-lg overflow-hidden shadow-2xl h-[400px]"
              style={{ 
                transform: `translateY(${Math.min(0, -(scrollY - 600) * 0.1)}px)`,
                transition: 'transform 0.1s linear'
              }}
            >
              {latestPost.frontmatter.coverImage && (
                <img 
                  src={latestPost.frontmatter.coverImage} 
                  alt={latestPost.frontmatter.title}
                  className="w-full h-full object-cover hover:scale-105 transition-transform duration-700"
                />
              )}
            </div>
            <div
              style={{ 
                transform: `translateY(${Math.min(0, -(scrollY - 600) * 0.15)}px)`,
                transition: 'transform 0.1s linear'
              }}
            >
              <div className="flex gap-2 mb-4">
                {latestPost.frontmatter.tags.map((tag, i) => (
                  <span key={i} className="badge badge-primary">
                    {tag}
                  </span>
                ))}
              </div>
              <h3 className="text-4xl font-bold mb-4 hover:text-primary transition-colors cursor-pointer" onClick={() => onPostClick(latestPost.slug)}>
                {latestPost.frontmatter.title}
              </h3>
              <p className="text-base-content opacity-80 mb-6 text-lg">
                {latestPost.frontmatter.excerpt}
              </p>
              <div className="flex justify-between items-center">
                <div className="flex items-center gap-4">
                  <div className="avatar">
                    <div className="w-12 h-12 rounded-full bg-primary flex items-center justify-center text-primary-content font-bold text-lg">
                      {latestPost.frontmatter.author.charAt(0)}
                    </div>
                  </div>
                  <div>
                    <p className="font-medium">{latestPost.frontmatter.author}</p>
                    <p className="text-sm opacity-70">{new Date(latestPost.frontmatter.date).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })}</p>
                  </div>
                </div>
                <button 
                  className="btn btn-primary"
                  onClick={() => onPostClick(latestPost.slug)}
                >
                  Read More
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      {/* All posts grid */}
      <div className="py-24 bg-base-100">
        <div className="container mx-auto px-4">
          <div className="mb-16 overflow-hidden">
            <h2 
              className="text-5xl md:text-6xl lg:text-7xl font-black mb-8 text-base-content opacity-90"
              style={{ 
                transform: `translateX(${Math.min(0, (scrollY - 1000) * 0.2)}px)`,
                transition: 'transform 0.1s linear'
              }}
            >
              ALL ARTICLES
            </h2>
            <div className="h-1 w-32 bg-primary rounded"></div>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {remainingPosts.map((post) => (
              <BlogPostSummary 
                key={post.slug}
                post={post}
                onClick={() => onPostClick(post.slug)}
              />
            ))}
          </div>
        </div>
      </div>
      
      {/* Newsletter section */}
      <div className="py-24 bg-primary text-primary-content">
        <div className="container mx-auto px-4 max-w-4xl text-center">
          <h2 className="text-4xl md:text-5xl font-black mb-6">Stay Updated</h2>
          <p className="text-xl mb-8 opacity-90">Subscribe to our newsletter to get the latest updates on F# development and more.</p>
          
          <div className="flex flex-col md:flex-row gap-4 max-w-2xl mx-auto">
            <input 
              type="email" 
              placeholder="Your email address" 
              className="input input-lg flex-grow text-base-content" 
            />
            <button className="btn btn-secondary btn-lg">Subscribe</button>
          </div>
        </div>
      </div>
      
      {/* Bottom scrolling marquee */}
      <div className="bg-base-200 py-4 overflow-hidden whitespace-nowrap">
        <div className="inline-block animate-marquee3">
          ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES -
        </div>
        <div className="inline-block animate-marquee4">
          ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES - FRESH NEWS & ARTICLES -
        </div>
      </div>
    </div>
  );
};

export default BlogIndex; 