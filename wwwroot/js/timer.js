"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/timerhub").build();


connection.on("UpdateTime", function (user, message) {
    console.log("Received message " + user);
    document.getElementById("timer").firstChild.nodeValue = user;
});

connection.start().then(function () {
    // document.getElementById("sendButton").disabled = false;
    console.log("connection started");
    document.getElementById("timer").firstChild.nodeValue = "starting timer...";
}).catch(function (err) {
    return console.error(err.toString());
});