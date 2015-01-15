/*jshint node:true*/
'use strict';

var express = require('express');
var app = express();
var http = require('http').Server(app);
var bodyParser = require('body-parser');
var favicon = require('serve-favicon');
var logger = require('morgan');
var port = process.env.PORT || 8001;
var path = require('path');
var api = require('./api.js');
var chatRoom = require('./sockets/socket.js');
var mongo = require('mongodb');
 
var environment = process.env.NODE_ENV;

app.use(favicon(__dirname + '/pg.ico'));
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());
app.use(logger('dev'));

app.use(express.static('./src/client/'));
app.use(express.static('./'));

path.resolve(__dirname+'.../client/index.html');

//TODO:  better way to manage routes
app.get('/api/rules', function(req, res){	 
	res.json(api.getRules());
});

app.get('/api/deck', function(req, res){	 
	res.json(api.getDeck());
});

app.get('/api/user', api.getUsers);

// Application routes
app.get('/game', function(req, res){
	res.sendfile('../client/game.html', {'root': '../fortyfives/src/client/'});
});

app.get('/', function(req, res){
	res.sendfile('../client/index.html', {'root': '../client/'});
});

http.listen(port, function() {
    console.log('Express server listening on port ' + port);
    console.log('env = ' + app.get('env') +
        '\n__dirname = ' + __dirname  +
        '\nprocess.cwd = ' + process.cwd());
});

chatRoom.initCommunication(http);



