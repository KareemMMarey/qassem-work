# qassim-province static project

## Description

Static website for Saudi Business Center

## Prerequisites

- [Node.js](https://nodejs.org/)
- [npm (Node Package Manager)](https://www.npmjs.com/)

## Getting Started

### Clone the Repository

Clone this repository to your local machine.

```bash
git clone https://github.com/aliigamall/qassim-province.git
```

### Navigate to Project Directory

```
cd qassim-province
```

### Install Dependencies

Install the necessary packages for the project.

```
npm install
```

### Deploy all assets

Execute the following command to deploy all assets to dist folder.

```
npm run build
```

### Run Development Server

Execute the following command to start the development server. It will automatically open your default web browser and navigate to the project.

```
npm start
```

## Project Structure

```
dist/                        Output directory where compiled HTML and CSS files are saved.
src/                         Source directory containing Pug and Sass files.
|- assets/                   Assets folder
|  |- fonts                  Directory for Fonts files
|  |- images                 Directory for Images / Videos files
|  |- scripts                Directory for Scripts files
|  |- scss                   Directory for Sass files
|- templates/                Directory for Pug template files
|  |- partials               Directory for Partials and components used in the pages
|  index.pug                 All pages to be here
gulpfile.js                  File defining Gulp tasks for automating the build process.
```

## Main tasks

| Task                                                           | Description                                 |
| -------------------------------------------------------------- | ------------------------------------------- |
| `npm start`                                                    | Run development server using browserSync    |
| `npm build`                                                    | build and deploy assets to dist folder      |
| `gulp pug`                                                     | Task to compile Pug to HTML with sourcemaps |
| `gulp sass`                                                    | Task to compile Sass to CSS with sourcemaps |
| -------------------------------------------------------------- |

## Author

Ali Gamal
