var gulp = require('gulp');
var del = require('del');
var msbuild = require('gulp-msbuild');
var nunit = require('gulp-nunit-runner');
var shell = require('gulp-shell');
var args = require('yargs').argv;

var configuration = args.configuration || 'Release';

gulp.task('clean', function() {
  del.sync(['src/**/bin/*', 'src/**/obj/*']);
});

gulp.task('build', ['clean'], function() {
  return gulp
    .src(['src/SeptaBus.sln'])
    .pipe(msbuild({
      targets: ['Clean', 'Build'],
      errorOnFail: true,
      stdout: true,
      properties: {
        Configuration: configuration
      }
    }));
});

// Ensures the NUnit console runner is present.
gulp.task('restoreSlnPackages', shell.task([
  'src\\.nuget\\NuGet.exe restore src\\.nuget\\packages.config -PackagesDirectory src\\packages'
]));

gulp.task('test', ['restoreSlnPackages', 'build'], function () {
  return gulp
    .src(['src/**/bin/' + configuration + '/*.Tests.dll'], { read: false })
    .pipe(nunit({
      executable: 'src/packages/NUnit.Runners.2.6.4/tools/nunit-console.exe'
    }));
});


gulp.task('default', ['test']);
