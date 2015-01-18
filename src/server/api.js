var express = require('express');
var fs = require('fs');
var app = express();
var mongoose = require('mongoose');
var mongo = require('mongodb');

var Server = mongo.Server, Db = mongo.Db, BSON = mongo.BSONPure;

var server = new Server('mongodb.pierrethelusma.com', 27017, {auto_reconnect: true});
db = new Db('FortyFivesDB', server);

db.open(function(err, db) {
	if(!err) {
		console.log("Connected to 'FortyFivesDB' database");
		db.collection('Users', {strict:true}, function(err, collection) {
			if (err) {
				console.log("The 'users' collection doesn't exist...");
			}
		});
	}
});

exports.getUsers = function(req, res) {
	db.collection('Users', function(err, collection) {
		collection.find().toArray(function(err, items) {
			res.send(items);
		});
	});
};









