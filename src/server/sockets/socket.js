(function () {
  exports.initCommunication = function(http){
    var io = require('socket.io')(http);

  // usernames which are currently connected to the chat
    var usernames = {};
    var numUsers = 0;


    io.on('connection', function(socket){
      var addedUser = false;


      socket.on('new message', function(data){
        socket.broadcast.emit('new message', {
          username: socket.username,
          message: data
        })
      });


      socket.on('add user', function(username){
        socket.username = username;
        usernames[username] = username;
        ++numUsers;
        addedUser = true;
        socket.emit('login', {
          numUsers: numUsers
        });
        // echo globally (all clients) that a person has connected
        socket.broadcast.emit('user joined', {
          username: socket.username,
          numUsers: numUsers
        });
      });

      // when the user disconnects.. perform this
      socket.on('disconnect', function () {
        // remove the username from global usernames list
        if (addedUser) {
          delete usernames[socket.username];
          --numUsers;

          // echo globally that this client has left
          socket.broadcast.emit('user left', {
            username: socket.username,
            numUsers: numUsers
          });
        }
      });

      // when the client emits 'typing', we broadcast it to others
      socket.on('typing', function () {
        socket.broadcast.emit('typing', {
          username: socket.username
        });
      });

      // when the client emits 'stop typing', we broadcast it to others
      socket.on('stop typing', function () {
        socket.broadcast.emit('stop typing', {
          username: socket.username
        });
      });
    });
}

}());

