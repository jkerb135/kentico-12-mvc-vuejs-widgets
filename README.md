# Kentico 12 MVC VueJs Widgets

This repository demos the use of rendering Kentico 12 MVC widgets in the Administration Pages Module with VueJs

## Core Features

All functionality is in the Ken120.Web.Mvc ASP.NET MVC project

- Content/InlineEditors
  - This is the location of the JavaScript integration code for the widget inline editors
    - Ex) `MediaSelectionEditor`
  - This is also the output location for the webpack build process for the Vue.js based inline editors (`inline-editors-bundle.dist.js`)
- Features/Home
  - This is the only feature/route in the application that displays content from the site
- Features/PageBuilders/InlineEditors
  - Contains the inline editor view models used to store / access inline editor state configured through widgets
    - Ex) `MediaSelectionEditorViewModel`
- Features/PageBuilders/Widgets
  - Contains the widget types that render either an inline editor when in editing mode or the actual content when on the public site
    - Includes view models, controllers, properties, queries
- Views/Home
  - Home feature index view where widgets can be rendered inside a section
- Views/Shared/InlineEditors
  - Views to translate server side view model data to client side javascript data for inline editor state
- Views/Shared/Widgets
  - Views to translate service side view model data to inline editors or rendered content for widgets
- package.json and webpack.config.js
  - Define the client asset build commands and pipeline 
  - Use `npm start` to run the build process for the Vue.Js widgets in watch mode
  - Use `npm run build` to create a dist / production build of the Vue.Js widget code