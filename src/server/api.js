var express = require('express');
 var fs = require('fs');
var app = express();

exports.getRules = function() {
	 var jsonObj = require("./mockData.json");
	 return jsonObj;
};
 


