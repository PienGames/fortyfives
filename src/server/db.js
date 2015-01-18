var mongoose = require('mongoose');
mongoose.connect('mongodb://localhost/FortyFivesDB', function(){
    console.log('mongodb connected')
});

module.exports = mongoose;
