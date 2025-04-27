import React from 'react';
import { Components } from 'react-markdown';
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter';
import { atomDark } from 'react-syntax-highlighter/dist/esm/styles/prism';

// Define props for the custom MDX components
interface CodeBlockProps {
  children: string;
  className?: string;
}

interface ImageProps {
  src: string;
  alt: string;
  className?: string;
}

interface LinkProps {
  href: string;
  children: React.ReactNode;
}

interface CalloutProps {
  type?: 'info' | 'warning' | 'tip' | 'note';
  children: React.ReactNode;
}

// Helper function to generate ID for headings
const generateId = (text: string): string => {
  return text
    .toLowerCase()
    .replace(/[^\w\s-]/g, '')
    .replace(/\s+/g, '-');
};

// Custom components for markdown rendering
const MarkdownComponents: Partial<Components> = {
  // Code blocks with syntax highlighting
  code({ className, children, ...props }: any) {
    const match = /language-(\w+)/.exec(className || '');
    const inline = !match;
    return !inline ? (
      <SyntaxHighlighter
        // @ts-ignore
        style={atomDark}
        language={match ? match[1] : ''}
        PreTag="div"
        className="rounded-md my-6"
        {...props}
      >
        {String(children).replace(/\n$/, '')}
      </SyntaxHighlighter>
    ) : (
      <code className="bg-base-300 px-1.5 py-0.5 rounded text-sm" {...props}>
        {children}
      </code>
    );
  },
  
  // Headings with anchors
  h1({ children }) {
    return <h1 className="text-4xl font-bold mt-8 mb-4">{children}</h1>;
  },
  
  h2({ children }) {
    if (!children) return <h2 className="text-3xl font-bold mt-8 mb-3"></h2>;
    
    const childrenText = children.toString();
    const id = generateId(childrenText);
    
    return (
      <h2 id={id} className="text-3xl font-bold mt-8 mb-3 group">
        {children}
        <a 
          href={`#${id}`} 
          className="opacity-0 group-hover:opacity-100 transition-opacity ml-1 text-primary"
          aria-hidden="true"
        >
          #
        </a>
      </h2>
    );
  },
  
  h3({ children }) {
    if (!children) return <h3 className="text-2xl font-bold mt-6 mb-3"></h3>;
    
    const childrenText = children.toString();
    const id = generateId(childrenText);
    
    return (
      <h3 id={id} className="text-2xl font-bold mt-6 mb-3 group">
        {children}
        <a 
          href={`#${id}`} 
          className="opacity-0 group-hover:opacity-100 transition-opacity ml-1 text-primary"
          aria-hidden="true"
        >
          #
        </a>
      </h3>
    );
  },
  
  h4({ children }) {
    return <h4 className="text-xl font-bold mt-4 mb-2">{children}</h4>;
  },
  
  // Block elements
  p({ children }) {
    return <p className="my-4 leading-relaxed">{children}</p>;
  },
  
  ul({ children }) {
    return <ul className="list-disc list-inside my-4 ml-4 space-y-2">{children}</ul>;
  },
  
  ol({ children }) {
    return <ol className="list-decimal list-inside my-4 ml-4 space-y-2">{children}</ol>;
  },
  
  li({ children }) {
    return <li className="mb-1">{children}</li>;
  },
  
  blockquote({ children }) {
    return <blockquote className="border-l-4 border-primary pl-4 py-2 italic my-4">{children}</blockquote>;
  },
  
  // Inline elements
  a({ href, children }) {
    if (!href) return <a>{children}</a>;
    
    const isExternal = href.startsWith('http');
    return (
      <a
        href={href}
        className="text-primary hover:text-primary/80 underline transition-colors"
        target={isExternal ? '_blank' : undefined}
        rel={isExternal ? 'noopener noreferrer' : undefined}
      >
        {children}
      </a>
    );
  },
  
  strong({ children }) {
    return <strong className="font-bold">{children}</strong>;
  },
  
  em({ children }) {
    return <em className="italic">{children}</em>;
  },
  
  // Custom image handling
  img({ src, alt, title }) {
    if (!src) return null;
    
    return (
      <figure className="my-8">
        <img
          src={src}
          alt={alt || ''}
          title={title || ''}
          className="rounded-lg w-full"
          loading="lazy"
        />
        {alt && <figcaption className="text-center text-sm mt-2 opacity-70">{alt}</figcaption>}
      </figure>
    );
  }
};

export default MarkdownComponents; 