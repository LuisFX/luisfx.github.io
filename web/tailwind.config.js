/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,ts,jsx,tsx,fs,fsx}",
    "./index.html",
  ],
  theme: {
    extend: {
      colors: {
        primary: "#3B82F6",
        secondary: "#10B981",
        accent: "#8B5CF6",
        neutral: "#191D24",
        "base-100": "#FFFFFF",
        info: "#3ABFF8",
        success: "#36D399",
        warning: "#FBBD23",
        error: "#F87272",
      },
      animation: {
        "gradient-x": "gradient-x 15s ease infinite",
        "gradient-y": "gradient-y 15s ease infinite",
        "gradient-xy": "gradient-xy 15s ease infinite",
      },
      keyframes: {
        "gradient-y": {
          "0%, 100%": {
            "background-size": "400% 400%",
            "background-position": "center top",
          },
          "50%": {
            "background-size": "200% 200%",
            "background-position": "center center",
          },
        },
        "gradient-x": {
          "0%, 100%": {
            "background-size": "200% 200%",
            "background-position": "left center",
          },
          "50%": {
            "background-size": "200% 200%",
            "background-position": "right center",
          },
        },
        "gradient-xy": {
          "0%, 100%": {
            "background-size": "400% 400%",
            "background-position": "left top",
          },
          "50%": {
            "background-size": "200% 200%",
            "background-position": "right bottom",
          },
        },
      },
    },
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      {
        light: {
          ...require("daisyui/src/theming/themes")["light"],
          primary: "#3B82F6",
          secondary: "#10B981",
          accent: "#8B5CF6",
        },
        dark: {
          ...require("daisyui/src/theming/themes")["dark"],
          primary: "#3B82F6",
          secondary: "#10B981", 
          accent: "#8B5CF6",
        },
      },
      "cyberpunk",
      "synthwave",
      "retro",
    ],
  },
} 