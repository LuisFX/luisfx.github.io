import React from 'react';
import { MDXProvider } from '@mdx-js/react';

const components = {
  h1: (props: any) => <h1 className="text-3xl font-bold my-4" {...props} />,
  h2: (props: any) => <h2 className="text-2xl font-bold my-3" {...props} />,
  h3: (props: any) => <h3 className="text-xl font-bold my-2" {...props} />,
  p: (props: any) => <p className="my-2" {...props} />,
  a: (props: any) => <a className="text-blue-500 hover:underline" {...props} />,
  ul: (props: any) => <ul className="list-disc ml-6 my-2" {...props} />,
  ol: (props: any) => <ol className="list-decimal ml-6 my-2" {...props} />,
  li: (props: any) => <li className="my-1" {...props} />,
  code: (props: any) => <code className="bg-gray-100 px-1 rounded" {...props} />,
  pre: (props: any) => <pre className="bg-gray-100 p-4 rounded my-4 overflow-auto" {...props} />,
  blockquote: (props: any) => <blockquote className="border-l-4 border-gray-300 pl-4 my-4 italic" {...props} />,
};

interface MDXProviderWrapperProps {
  children: React.ReactNode;
}

export function MDXProviderWrapper({ children }: MDXProviderWrapperProps) {
  return (
    <MDXProvider components={components}>
      {children}
    </MDXProvider>
  );
} 