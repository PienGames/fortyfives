(function () {
	var mongoose = require('mongoose');
	mongoose.connect('mongodb://mongodb.pierrethelusma.com/test', function(){
	    console.log('mongodb connected')
	    mongoose.connection.db.collectionNames(function (err, names) {
	        console.log(names);
	        module.exports.Collection = names;
	    });
	});

	module.exports = mongoose;
}());
