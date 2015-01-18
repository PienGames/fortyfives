/*jshint node:true*/
'use strict';

var express = require('express');
var app = express();
var http = require('http').Server(app);

var port = process.env.PORT || 8001;
var environment = process.env.NODE_ENV;

// initialization of API routes, fav icon, and express usage
var global = require('./server/global');
global.appStart(express);

// serve up all client directory items as static content
// access index with http://localhost:8001
// access game.html with http://localhost:8001/views/game.html
app.use(express.static(__dirname + '/client'));


app.use(require('./server/controllers/deck'));
app.use(require('./server/controllers/rules'));
app.use(require('./server/controllers/users'));

http.listen(port, function() {
    console.log('Express server listening on port ' + port);
    console.log('env = ' + app.get('env') +
        '\n__dirname = ' + __dirname  +
        '\nprocess.cwd = ' + process.cwd());
});

// initializes the socket communication
var chatRoom = require('./server/sockets/socket.js');
chatRoom.initCommunication(http);



