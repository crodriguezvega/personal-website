module.exports = function(grunt){
  // load plugins
  [
    'grunt-json-minify',
    'grunt-contrib-uglify',
    'grunt-contrib-cssmin',
    'grunt-processhtml',
    'grunt-hashres',
    'grunt-cdn',
  ].forEach(function(task){
    grunt.loadNpmTasks(task);
  });

  // configure plugins
  grunt.initConfig({
    'json-minify': {
      build: {
        files: '_site/json/**/*.json'
      }
    },
    uglify: {
      all: {
        files: {
          '_site/js/home-layout.min.js': [
            '_site/jquery/jquery.js',
            '_site/bootstrap/js/bootstrap.js'
          ],
          '_site/js/main-layout.min.js': [
            '_site/jquery/jquery.js',
            '_site/bootstrap/js/bootstrap.js',
            '_site/spin/spin.js',
            '_site/d3/d3.js',
            '_site/d3-tip/d3-tip.js'
          ],
          '_site/js/nba-salaries.min.js': [
            '_site/js/nba-salaries.js'
          ],
          '_site/js/wilco-shows.min.js': [
            '_site/knockout/knockout.js',
            '_site/moment/moment.js',
            '_site/js/wilco-shows.js'
          ],
          '_site/js/box-office.min.js': [
            '_site/d3-format/d3-format.js',
            '_site/moment/moment.js',
            '_site/js/box-office.js'
          ]
        }
      }
    },
    cssmin: {
      target: {
        files: {
          '_site/css/home-layout.min.css': [
            '_site/bootstrap/css/bootstrap.css',
            '_site/css/home-layout.css'
          ],
          '_site/css/main-layout.min.css': [
            '_site/bootstrap/css/bootstrap.css',
            '_site/css/main-layout.css',
            '_site/css/visualizations.css'
          ],
          '_site/css/nba-salaries.min.css': [
            '_site/css/nba-salaries.css'
          ],
          '_site/css/wilco-shows.min.css': [
            '_site/css/wilco-shows.css'
          ],
          '_site/css/box-office.min.css': [
            '_site/css/box-office.css'
          ]
        }
      }
    },
    processhtml: {
      target: {
        files: {
          '_site/index.html': ['_site/index.html'],
          '_site/visualizations/nba-salaries/index.html': ['_site/visualizations/nba-salaries/index.html'],
          '_site/visualizations/wilco-shows/index.html': ['_site/visualizations/wilco-shows/index.html'],
          '_site/visualizations/box-office/index.html': ['_site/visualizations/box-office/index.html']
        }
      },
    },
    hashres: {
      options: {
        fileNameFormat: '${name}.${hash}.${ext}'
      },
      all: {
        src: [
          '_site/js/home-layout.min.js',
          '_site/js/main-layout.min.js',
          '_site/js/nba-salaries.min.js',
          '_site/js/wilco-shows.min.js',
          '_site/js/box-office.min.js',
          '_site/css/home-layout.min.css',
          '_site/css/main-layout.min.css',
          '_site/css/nba-salaries.min.css',
          '_site/css/wilco-shows.min.css',
          '_site/css/box-office.min.css'
        ],
        dest: [
          '_site/index.html',
          '_site/visualizations/nba-salaries/index.html',
          '_site/visualizations/wilco-shows/index.html',
          '_site/visualizations/box-office/index.html'
        ]
      },
    },
    cdn: {
      options: {
        cdn: 'https://d2gkkp7311831a.cloudfront.net',
      },
      dist: {
        cwd: '_site/',
        dest: '_site/',
        src: ['**/*.css', '**/*.html']
      }
    },
  });

  // register tasks
  grunt.registerTask('default', []);
  grunt.registerTask('assets', ['json-minify', 'uglify', 'cssmin', 'processhtml', 'hashres', 'cdn']);
};
