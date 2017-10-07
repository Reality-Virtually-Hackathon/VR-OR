#!/bin/node

/**
 * VR-OR
 *
 * @file server.js
 * @author Uwe Gruenefeld
 * @version 2017-10-06
 **/

 // create objects
 var express = require('express')
 ,   app = express()
 ,   server = require('http').createServer(app)
 ,   io = require('socket.io').listen(server)
 ,   conf = require('./config.json');

 // bind port to server
 server.listen(conf.port);

 // static files
 app.use(express.static('./content/'));

 // index file
 app.get('/', function (req, res) {
 	res.sendfile('./content/index.htm');
 });

 // data management
 io.sockets.on('connection', function (socket) {

 	// get of variable
 	socket.on('data', function(data) {


 	});
 });

 // status
 console.log('Server is running http://localhost:' + conf.port + '/');
