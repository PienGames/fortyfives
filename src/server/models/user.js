(function () {
    var db = require('../db');
    var Schema = db.Schema;
    var UserSchema = new Schema({
        version: Number,
        username: String,
        password: String,
        email: String,
        date: { type: Date, default: Date.now },
        games: {
            gamesPlayed: Number,
            gamesWon:  Number
        }
    });

    UserSchema.statics.findByUserName = function (username, cb){
        this.find({ username: new RegExp(username, 'i') }, cb);
    };

    UserSchema.statics.findByEmail = function (email, cb){
        this.find({ email: new RegExp(email, 'i') }, cb);
    };

    var User = db.model('Users', UserSchema);
    module.exports = User;
}());


