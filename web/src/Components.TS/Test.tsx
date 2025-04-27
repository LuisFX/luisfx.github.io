// This is a test component

import React from 'react';
import { useState } from 'react'

export const Test = () => {
  const [count, setCount] = useState(0)
  return (
    <div>
      <h1>Test</h1>
      <p>{count}</p>
      <button onClick={() => setCount(count + 1)}>Click me</button>
    </div>
  )
}

export default Test

