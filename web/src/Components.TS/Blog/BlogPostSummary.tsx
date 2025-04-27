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
      className="card bg-base-200 shadow-xl hover:shadow-2xl transition-all duration-300 h-full cursor-pointer overflow-hidden"
      onClick={onClick}
    >
      {post.frontmatter.coverImage && (
        <figure className="h-48 overflow-hidden">
          <img 
            src={post.frontmatter.coverImage} 
            alt={post.frontmatter.title}
            className="w-full h-full object-cover transition-transform duration-300 hover:scale-105"
          />
        </figure>
      )}
      
      <div className="card-body">
        <div className="flex gap-2 mb-2">
          {post.frontmatter.tags.slice(0, 3).map((tag, index) => (
            <span key={index} className="badge badge-primary badge-sm">
              {tag}
            </span>
          ))}
          {post.frontmatter.tags.length > 3 && (
            <span className="badge badge-ghost badge-sm">+{post.frontmatter.tags.length - 3}</span>
          )}
        </div>
        
        <h2 className="card-title text-2xl font-bold hover:text-primary transition-colors">
          {post.frontmatter.title}
        </h2>
        
        <div className="flex items-center gap-2 text-sm opacity-70 mt-1">
          <span>{formatDate(post.frontmatter.date)}</span>
          <span className="text-xs">•</span>
          <span>{readingTime} min read</span>
        </div>
        
        <p className="mt-3 line-clamp-3 text-opacity-80 text-base-content">
          {post.frontmatter.excerpt}
        </p>
        
        <div className="card-actions justify-between items-center mt-4">
          <div className="flex items-center">
            <div className="avatar">
              <div className="w-8 h-8 rounded-full bg-primary flex items-center justify-center text-primary-content font-bold">
                {post.frontmatter.author.charAt(0)}
              </div>
            </div>
            <span className="ml-2 text-sm">{post.frontmatter.author}</span>
          </div>
          
          <button className="btn btn-sm btn-primary">
            Read More
          </button>
        </div>
      </div>
    </div>
  );
};

export default BlogPostSummary; 