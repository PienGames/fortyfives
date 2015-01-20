(function () {
    var User = require('../models/user');
    var router = require('express').Router();

    router.get('/api/users', function (req, res){
        User.find()
            .sort('-date')
            .exec(function (err, users){
                if (err){
                    return next(err);
                }
                res.status(200).json(users);
            });
    });

    router.get('/api/users/:email_address', function(req, res) {
        User.findByEmail(req.params.email_address, function (err, user) {
            if (err){
                return next(err);
            }
            res.status(200).json(user);
        });
    });

    router.post('/api/users', function (req, res){
        var user = new User({
            username: req.body.username,
            password: req.body.password,
            email: req.body.email,
            version: 1,
            games: {
                gamesPlayed: 1,
                gamesWon: 0
            }
        });

        user.save(function (err, savedUser){
            if (err){
                return next(err);
            }
            res.status(201).json(savedUser);
        });

    });

    module.exports = router;

}());


