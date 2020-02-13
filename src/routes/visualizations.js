'use strict'

var express = require('express');
var router = express.Router();

var allowedIds = ['nba-salaries', 'wilco-shows', 'box-office'];
var titles = ['NBA Salaries', 'Wilco\'s Shows', 'Highest-grossing weekend US box office'];

router.get('/:id([a-z\-]+)', function(req, res, next) {
  var id = req.params.id.toLowerCase();
  var index = allowedIds.indexOf(id);

  if (index >= 0) {
    res.render('visualizations/' + id, { title: titles[index], assetsUrl: req.assetsUrl });
  }
  else {
    next();
  }
});

module.exports = router;