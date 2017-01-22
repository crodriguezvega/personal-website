'use strict'

var express = require('express');
var router = express.Router();

var allowedIds = ['nba-salaries', 'wilco-shows'];
var titles = ['NBA salaries', 'Wilco shows'];

router.get('/:id([a-z\-]+)', function(req, res, next) {
  var id = req.params.id.toLowerCase();
  
  if (allowedIds.indexOf(id) >= 0) {
    res.render('visualizations/' + id, { title: titles[id] });
  }
  else {
    next();
  }
});

module.exports = router;