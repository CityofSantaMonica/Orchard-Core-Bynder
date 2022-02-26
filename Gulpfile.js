const recommendedSetup = require('../../Utilities/Lombiq.Gulp.Extensions/recommended-setup');

const assets = [
    {
        name: 'images',
        path: './Assets/Images/**/*',
    },
];

recommendedSetup.setupRecommendedScssAndJsTasksAndCopyAssets(assets);
gulp.task('copy:assets', () => copyAssets(assets, distBasePath));
