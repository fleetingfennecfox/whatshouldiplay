/// <binding BeforeBuild='clean, public-js' ProjectOpened='watch' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var tasks = { public: "public-js" }

var gulp = require('gulp'),
    gutil = require('gulp-util');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var watch = require('gulp-watch');
var wrap = require('gulp-wrap');
var del = require('del');

var config = {

    public: {
        js: {
            src: [
                // main module
                'app/public/app.public.js',

                // module files
                'app/public/modules/**/*.js',

                // common module files
                //'app/common/modules/**/*.js',
            ]
        }
    }
}

var destinations = {
    public: {
        js: 'scripts/dest/public'
    },

}
//delete the output file(s)
gulp.task('clean', function () {
    del.sync(destinations.public.js);
});

gulp.task(tasks.public, function () {

    return gulp.src(config.public.js.src)
        // .pipe(uglify())
        .pipe(wrap('\n//<%= file.relative %>\n<%= contents %>'))
        .pipe(concat('app.public.js'))
        .pipe(gulp.dest(destinations.public.js))
})

gulp.task('watch', function () {
    gulp.start(tasks.public);

    //gulp.watch(config.public.js.src, { cwd: config.public.js.src }, [tasks.public]);
    gulp.watch(config.public.js.src, [tasks.public]);

});

