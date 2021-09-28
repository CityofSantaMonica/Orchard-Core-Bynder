const gulp = require('gulp');
const babel = require('gulp-babel');
const watch = require('gulp-watch');
const jsTargets = require('../../Utilities/Lombiq.Gulp.Extensions/Tasks/js-targets');
const scssTargets = require('../../Utilities/Lombiq.Gulp.Extensions/Tasks/scss-targets');
const copyAssets = require('../../Utilities/Lombiq.Gulp.Extensions/Tasks/copy-assets');

const assetsBasePath = './Assets/';
const distBasePath = './wwwroot/';
const scriptsBasePath = assetsBasePath + 'Scripts/';
const scriptsDistBasePath = distBasePath + 'js/';
const stylesBasePath = assetsBasePath + 'Styles/';
const stylesDistBasePath = distBasePath + 'css/';
const imagesBasePath = assetsBasePath + 'Images/';
const imagesGlob = imagesBasePath + '**/*';

const assets = [
    {
        name: 'images',
        path: imagesGlob,
    },
];

gulp.task(
    'build:scripts',
    () => jsTargets.compile(
        scriptsBasePath, scriptsDistBasePath, (pipeline) => pipeline.pipe(babel({ presets: ['@babel/preset-env'] }))));

gulp.task('build:styles', scssTargets.build(stylesBasePath, stylesDistBasePath));

gulp.task('copy:assets', () => copyAssets(assets, distBasePath));

gulp.task('default', gulp.parallel('build:scripts', 'build:styles', 'copy:assets'));

gulp.task('watch', () => {
    watch(scriptsBasePath + '**/*.js', { verbose: true }, gulp.series('build:scripts'));
    watch(stylesBasePath + '**/*.scss', { verbose: true }, gulp.series('build:styles'));
    watch(imagesGlob, { verbose: true }, gulp.series('build:scripts'));
});
