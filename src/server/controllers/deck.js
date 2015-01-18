(function () {
	var router = require('express').Router();

	router.get('/api/deck', function (req, res){
	    var deck = require("../rules/deck.json");
	    res.status(200).json(deck);
	});

	module.exports = router;
}());