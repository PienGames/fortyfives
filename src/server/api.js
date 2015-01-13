var express = require('express');
var fs = require('fs');
var app = express();

exports.getDeck = function() {
	 var jsonObj = require("./deck.json");
	 return jsonObj;
};
 
 exports.getRules = function() {
	 var jsonObj = require("./cardOrder.json");
	 return jsonObj;
};

var mongoose = require('mongoose');
mongoose.connect('mongodb://localhost/FortyFivesDB');

var Cat = mongoose.model('Cat', { name: String });

var kitty = new Cat({ name: 'Zildjian' });
kitty.save(function (err) {
  console.dir('meow');
});






