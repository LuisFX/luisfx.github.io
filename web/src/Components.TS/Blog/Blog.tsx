import React, { useState } from 'react';
import BlogIndex from './BlogIndex';
import BlogPost from './BlogPost';

interface BlogProps {
  // Optional initial slug if you want to display a specific post
  initialSlug?: string;
}

const Blog: React.FC<BlogProps> = ({ initialSlug }) => {
  const [currentSlug, setCurrentSlug] = useState<string | null>(initialSlug || null);
  
  // Handle navigating to a specific post
  const handlePostClick = (slug: string) => {
    setCurrentSlug(slug);
    window.scrollTo(0, 0);
  };
  
  // Handle navigating back to the blog index
  const handleBackClick = () => {
    setCurrentSlug(null);
    window.scrollTo(0, 0);
  };
  
  return (
    <div className="min-h-screen">
      {currentSlug ? (
        <BlogPost slug={currentSlug} onBack={handleBackClick} />
      ) : (
        <BlogIndex onPostClick={handlePostClick} />
      )}
    </div>
  );
};

export default Blog; 