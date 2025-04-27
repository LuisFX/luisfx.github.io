import React, { useEffect, useState } from 'react';
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
      <div className="flex justify-center items-center min-h-[50vh]">
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

  return (
    <div className="container mx-auto px-4 py-12 max-w-7xl">
      <div className="flex flex-col items-center mb-16">
        <h1 className="text-5xl md:text-6xl font-black mb-4 text-center">Blog</h1>
        <p className="text-xl opacity-70 max-w-2xl text-center">
          Thoughts, stories and ideas about F#, web development, and functional programming.
        </p>
      </div>
      
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        {posts.map((post) => (
          <BlogPostSummary 
            key={post.slug}
            post={post}
            onClick={() => onPostClick(post.slug)}
          />
        ))}
      </div>
    </div>
  );
};

export default BlogIndex; 