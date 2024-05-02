const gulp = require('gulp');
const pug = require('gulp-pug');
const sass = require('gulp-sass')(require('sass'));
const sourcemaps = require('gulp-sourcemaps');
const browserSync = require('browser-sync').create();

// Task to compile Pug to HTML with sourcemaps
gulp.task('pug', function () {
    return gulp.src(['src/templates/**/*.pug', '!src/templates/**/_*.pug']) // Exclude partials
        .pipe(sourcemaps.init())
        .pipe(pug({
            pretty: true
        }))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('dist'))
        .pipe(browserSync.stream());
});

// Task to compile Sass to CSS with sourcemaps
gulp.task('sass', function () {
    return gulp.src('src/assets/scss/**/*.scss') // Watching in all subdirectories
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('dist/assets/css'))
        .pipe(browserSync.stream());
});


// Task to copy the fonts folder to dist
gulp.task('deployFonts', function () {
    return gulp.src('src/assets/fonts/**/*')
        .pipe(gulp.dest('dist/assets/fonts'));
});

// Task to copy images to dist
gulp.task('deployImages', function () {
    return gulp.src('src/assets/images/**/*') // Get all images, including subdirectories
        .pipe(gulp.dest('dist/assets/images')); // Output to dist/images directory
});

// Task to copy scripts to dist
gulp.task('deoloyScripts', function () {
    return gulp.src(['src/assets/scripts/**/*'])
        .pipe(gulp.dest('dist/assets/scripts'));
});

// Task to serve the project and watch for changes
gulp.task('serve', function () {
    browserSync.init({
        server: {
            baseDir: 'dist'
        }
    });

    gulp.watch('src/templates/**/*.pug', gulp.series('pug')); // Watching in all subdirectories
    gulp.watch('src/assets/scss/**/*.scss', gulp.series('sass')); // Watching in all subdirectories
    gulp.watch('src/assets/scripts/**/*.js', gulp.series('deoloyScripts')); // Watching in all subdirectories
    gulp.watch('dist/**/*').on('change', browserSync.reload);
});


// Deploy task to run all necessary tasks for deploying the project
gulp.task('deploy', gulp.series('pug', 'sass', 'deployFonts', 'deployImages', 'deoloyScripts'));
