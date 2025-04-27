import manifestData from '../../blogpost.manifest.json';

// Define BlogPost type
export interface BlogPostFrontmatter {
  title: string;
  date: string;
  author: string;
  tags: string[];
  excerpt: string;
  coverImage?: string;
}

export interface BlogPost {
  slug: string;
  filename: string;
  frontmatter: BlogPostFrontmatter;
  content?: string;
}

interface Manifest {
  posts: BlogPost[];
}

// Get all the slugs for blog posts
export const getPostSlugs = (): string[] => {
  try {
    const manifest = manifestData as Manifest;
    return manifest.posts.map(post => post.slug);
  } catch (error) {
    console.error('Error getting post slugs:', error);
    return [];
  }
};

// Get a single post by slug
export const getPostBySlug = async (slug: string): Promise<BlogPost | null> => {
  try {
    const manifest = manifestData as Manifest;
    const postMetadata = manifest.posts.find(post => post.slug === slug);
    
    if (!postMetadata) {
      console.error(`Post with slug "${slug}" not found in manifest`);
      return null;
    }
    
    // Dynamically load the MDX file content
    try {
      const postContent = await import(`../../../blog/posts/${postMetadata.filename}`);
      
      // Return post with content
      return {
        ...postMetadata,
        content: postContent.default
      };
    } catch (error) {
      console.error(`Error loading MDX file for "${slug}":`, error);
      return postMetadata; // Return just the metadata if content loading fails
    }
  } catch (error) {
    console.error(`Error getting post with slug "${slug}":`, error);
    return null;
  }
};

// Get all posts
export const getAllPosts = async (): Promise<BlogPost[]> => {
  try {
    const manifest = manifestData as Manifest;
    
    return manifest.posts.sort((postA, postB) => {
      // Sort by date in descending order
      const dateA = new Date(postA.frontmatter.date);
      const dateB = new Date(postB.frontmatter.date);
      return dateB.getTime() - dateA.getTime();
    });
  } catch (error) {
    console.error('Error getting all posts:', error);
    return [];
  }
};

// Generate table of contents from markdown content
export const generateTableOfContents = (content: string): { text: string; id: string }[] => {
  const headingRegex = /^#+\s+(.*)$/gm;
  const toc: { text: string; id: string }[] = [];
  
  let match;
  while ((match = headingRegex.exec(content)) !== null) {
    const text = match[1];
    // Create an ID from the heading text
    const id = text
      .toLowerCase()
      .replace(/[^\w\s-]/g, '')
      .replace(/\s+/g, '-');
    
    toc.push({ text, id });
  }
  
  return toc;
}; 