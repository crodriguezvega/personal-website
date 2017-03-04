'use strict'

var fs = require('fs');
var path = require('path');
var swig  = require('swig');
var logger = require('morgan');
var express = require('express');
var favicon = require('serve-favicon');
var compression = require('compression');

var routes = require('./src/routes/index');
var visualizations = require('./src/routes/visualizations');

var isProduction = process.env.NODE_ENV === 'production';
var baseUrl = isProduction ? 'https://d2gkkp7311831a.cloudfront.net' : '';

var app = express();

// View engine setup
app.engine('html', swig.renderFile);
if (!isProduction) {
  swig.setDefaults({ cache: false });
}

app.set('view engine', 'html');

if (isProduction) {
  app.set('views', path.join(__dirname, 'dist/views'));

  app.use(logger('common', {
      skip: function(req, res) { return res.statusCode < 400 },
      stream: fs.createWriteStream(path.join(__dirname, 'morgan.log'), { flags: 'a' })
    })
  );
  app.use(compression());
  app.use(express.static(path.join(__dirname, 'dist')));
} else {
  app.set('views', path.join(__dirname, 'src/views'));

  app.use(logger('dev'));
  app.use(express.static(path.join(__dirname, 'src/public')));
  app.use(express.static(path.join(__dirname, 'src/vendor')));
}

app.use(function (req, res, next) {
  req.assetsUrl = baseUrl;
  next();
});

// Routes
app.use('/', routes);
app.use('/visualizations', visualizations);

// Catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// Development error handler -> will print stacktrace
if (!isProduction) {
  app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', { message: err.message, error: err });
  });
}

// Production error handler -> no stacktraces leaked to user
app.use(function(err, req, res, next) {
  res.status(err.status || 500);
  res.render('error', { message: err.message });
});

module.exports = app;