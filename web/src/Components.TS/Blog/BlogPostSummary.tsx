import React from 'react';
import { BlogPost } from './mdxUtils';

interface BlogPostSummaryProps {
  post: BlogPost;
  onClick: () => void;
}

const formatDate = (dateString: string): string => {
  const date = new Date(dateString);
  return date.toLocaleDateString('en-US', { 
    year: 'numeric', 
    month: 'long', 
    day: 'numeric' 
  });
};

// Calculate estimated reading time - uses excerpt if content is not available
const calculateReadingTime = (post: BlogPost): number => {
  const wordsPerMinute = 200;
  
  // If content is available, use it for more accurate reading time
  if (post.content) {
    // Remove frontmatter section and code blocks before counting words
    const cleanContent = post.content
      .replace(/^---[\s\S]*?---\s*/m, '') // Remove frontmatter
      .replace(/```[\s\S]*?```/g, '');    // Remove code blocks
      
    const words = cleanContent.trim().split(/\s+/).length;
    return Math.ceil(words / wordsPerMinute);
  }
  
  // Fallback to using excerpt and an average article length estimate
  const excerptWords = post.frontmatter.excerpt.trim().split(/\s+/).length;
  // Assume average blog post is about 1000 words if we only have excerpt
  return Math.ceil(Math.max(excerptWords * 5, 1000) / wordsPerMinute);
};

const BlogPostSummary: React.FC<BlogPostSummaryProps> = ({ post, onClick }) => {
  const readingTime = calculateReadingTime(post);
  
  return (
    <div 
      className="blog-card group bg-base-200 rounded-xl shadow-lg hover:shadow-2xl overflow-hidden flex flex-col cursor-pointer h-full"
      onClick={onClick}
    >
      <div className="image-hover h-56 overflow-hidden">
        {post.frontmatter.coverImage ? (
          <img 
            src={post.frontmatter.coverImage} 
            alt={post.frontmatter.title}
            className="w-full h-full object-cover transition-all duration-700"
          />
        ) : (
          <div className="w-full h-full bg-gradient-to-br from-primary/50 to-primary/20 flex items-center justify-center">
            <span className="text-5xl font-bold text-white/50">F#</span>
          </div>
        )}
      </div>
      
      <div className="p-6 flex-grow flex flex-col">
        <div className="flex flex-wrap gap-2 mb-3">
          {post.frontmatter.tags.slice(0, 3).map((tag, index) => (
            <span key={index} className="badge badge-sm badge-primary bg-opacity-80 text-xs font-medium">
              {tag}
            </span>
          ))}
          {post.frontmatter.tags.length > 3 && (
            <span className="badge badge-sm badge-ghost text-xs">+{post.frontmatter.tags.length - 3}</span>
          )}
        </div>
        
        <h2 className="text-xl font-bold mb-3 group-hover:text-primary transition-colors line-clamp-2">
          {post.frontmatter.title}
        </h2>
        
        <p className="text-base-content/80 text-sm mb-4 line-clamp-3 flex-grow">
          {post.frontmatter.excerpt}
        </p>
        
        <div className="flex items-center justify-between mt-auto pt-4 border-t border-base-300">
          <div className="flex items-center gap-3">
            <div className="avatar">
              <div className="w-8 h-8 rounded-full bg-primary/90 flex items-center justify-center text-primary-content font-semibold text-sm">
                {post.frontmatter.author.charAt(0)}
              </div>
            </div>
            <div>
              <span className="text-xs opacity-80 block">{formatDate(post.frontmatter.date)}</span>
              <span className="text-xs opacity-60">{readingTime} min read</span>
            </div>
          </div>
          
          <div className="text-primary text-xs font-semibold flex items-center">
            Read More
            <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 ml-1 group-hover:translate-x-1 transition-transform" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
            </svg>
          </div>
        </div>
      </div>
    </div>
  );
};

export default BlogPostSummary; 