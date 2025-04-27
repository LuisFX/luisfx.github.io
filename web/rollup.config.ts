/**
 * @import {RollupOptions} from 'rollup'
 */

import mdx from '@mdx-js/rollup'

/** @type {RollupOptions} */
const config = {
  // …
  plugins: [
    // …
    mdx({jsxImportSource: 'react'}
  ]
}

export default config