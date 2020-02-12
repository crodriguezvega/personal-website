module.exports = function(grunt){
  // load plugins
  [
    'grunt-contrib-copy',
    'grunt-json-minify',
    'grunt-contrib-uglify',
    'grunt-contrib-cssmin',
    'grunt-encode-css-images',
    'grunt-hashres',
    'grunt-processhtml',
    'grunt-cdn',
  ].forEach(function(task){
    grunt.loadNpmTasks(task);
  });

  // configure plugins
  grunt.initConfig({
    copy: {
      main: {
        files: [
          { expand: true, cwd: 'src/public/favicon', src: '**', dest: 'dist/favicon' },
          { expand: true, cwd: 'src/public/json', src: '**', dest: 'dist/json' },
          { expand: true, cwd: 'src/public/img', src: '**', dest: 'dist/img' },
          { expand: true, cwd: 'src/views', src: '**', dest: 'dist/views' }
        ]
      }
    },
    'json-minify': {
      build: {
        files: 'dist/json/**/*.json'
      }
    },
    uglify: {
      all: {
        files: {
          'dist/js/home-layout.min.js': [
            'src/vendor/jquery/jquery.js',
            'src/vendor/bootstrap/js/bootstrap.js'
          ],
          'dist/js/main-layout.min.js': [
            'src/vendor/jquery/jquery.js',
            'src/vendor/bootstrap/js/bootstrap.js',
            'src/vendor/spin/spin.js',
            'src/vendor/d3/d3.js',
            'src/vendor/d3-tip/d3-tip.js'
          ],
          'dist/js/nba-salaries.min.js': [
            'src/public/js/nba-salaries.js'
          ],
          'dist/js/wilco-shows.min.js': [
            'src/vendor/knockout/knockout.js',
            'src/vendor/moment/moment.js',
            'src/public/js/wilco-shows.js'
          ],
          'dist/js/box-office.min.js': [
            'src/vendor/d3-format/d3-format.js',
            'src/vendor/moment/moment.js',
            'src/public/js/box-office.js'
          ]
        }
      }
    },
    cssmin: {
      target: {
        files: {
          'dist/css/home-layout.min.css': [
            'src/vendor/bootstrap/css/bootstrap.css',
            'src/public/css/home-layout.css'
          ],
          'dist/css/main-layout.min.css': [
            'src/vendor/bootstrap/css/bootstrap.css',
            'src/public/css/main-layout.css',
            'src/public/css/visualizations.css'
          ],
          'dist/css/nba-salaries.min.css': [
            'src/public/css/nba-salaries.css'
          ],
          'dist/css/wilco-shows.min.css': [
            'src/public/css/wilco-shows.css'
          ],
          'dist/css/box-office.min.css': [
            'src/public/css/box-office.css'
          ]
        }
      }
    },
    'encode-css-images': {
      options: {
        imageDir: 'dist/'
      },
      target: {
        files: [{
          expand: true,
          cwd: 'dist/css/',
          src: '**/*.min.css',
          dest: 'dist/css/'
        }]
     }
    },
    processhtml: {
      target: {
        files: {
          'dist/views/home-layout.html': ['dist/views/home-layout.html'],
          'dist/views/main-layout.html': ['dist/views/main-layout.html'],
          'dist/views/visualizations/nba-salaries.html': ['dist/views/visualizations/nba-salaries.html'],
          'dist/views/visualizations/wilco-shows.html': ['dist/views/visualizations/wilco-shows.html'],
          'dist/views/visualizations/box-office.html': ['dist/views/visualizations/box-office.html']
        }
      },
    },
    hashres: {
      options: {
        fileNameFormat: '${name}.${hash}.${ext}'
      },
      all: {
        src: [
          'dist/js/home-layout.min.js',
          'dist/js/main-layout.min.js',
          'dist/js/nba-salaries.min.js',
          'dist/js/wilco-shows.min.js',
          'dist/js/box-office.min.js',
          'dist/css/home-layout.min.css',
          'dist/css/main-layout.min.css',
          'dist/css/nba-salaries.min.css',
          'dist/css/wilco-shows.min.css',
          'dist/css/box-office.min.css'
        ],
        dest: [
          'dist/views/home-layout.html',
          'dist/views/main-layout.html',
          'dist/views/visualizations/nba-salaries.html',
          'dist/views/visualizations/wilco-shows.html',
          'dist/views/visualizations/box-office.html'
        ]
      },
    },
    cdn: {
      options: {
        cdn: 'https://d2gkkp7311831a.cloudfront.net',
      },
      dist: {
        cwd: 'dist/',
        dest: 'dist/',
        src: ['**/*.css', '**/*.html']
      }
    },
  });	

  // register tasks
  grunt.registerTask('default', []);
  grunt.registerTask('assets', ['copy', 'json-minify', 'uglify', 'cssmin', 'processhtml', 'hashres', 'cdn']);
};