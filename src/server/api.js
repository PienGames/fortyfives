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
 


