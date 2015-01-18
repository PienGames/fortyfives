(function () {
	var router = require('express').Router();

	router.get('/api/rules', function (req, res){
	    var rules = require("../rules/cardOrder.json");
	    res.status(200).json(rules);
	});

	module.exports = router;

}());
