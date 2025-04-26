module Main

open Feliz
open Browser.Dom

// Load FontAwesome icons
// Tell webpack to load our script
importSideEffects "./styles.css"
importSideEffects "https://kit.fontawesome.com/92e9c888da.js"

let root = ReactDOM.createRoot(document.getElementById "feliz-app")
root.render(App.App.App())