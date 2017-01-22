'use strict'

var express = require('express');
var router = express.Router();

router.get('/', function(req, res, next) {
  var dt = new Date();
  res.render('index', { title: 'Home', year: dt.getFullYear() });
});

module.exports = router;
