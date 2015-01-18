exports.appStart = function(express){
    var app = express();
    var path = require('path');
    var favicon = require('serve-favicon');
    var bodyParser = require('body-parser');
    var logger = require('morgan');
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(bodyParser.json());
    app.use(logger('dev'));
    app.use(favicon(__dirname + '/pg.ico'));
};