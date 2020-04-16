"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/timerhub").build();

Notification.requestPermission().then(function(result) {
    console.log(result);
});


connection.on("UpdateTime", function (user, message) {
    console.log("Received message " + user);
    document.getElementById("timer").firstChild.nodeValue = user.message;
    document.getElementById("pause").disabled = !user.pauseEnabled;
    document.getElementById("resume").disabled = !user.resumeEnabled;
    document.getElementById("restart").disabled = !user.restartEnabled;
});

connection.on("TimerElapsed", function(){
    console.log("Timer elapsed");
    if(Notification.permission === "granted") {
        new Notification("Next mobber!", { tag: window.location, renotify: true });
    }
});

connection.start().then(function () {
    console.log("connection started");
    document.getElementById("timer").firstChild.nodeValue = "starting timer...";
}).catch(function (err) {
    return console.error(err.toString());
});

function pauseClicked()
{
    send("/timer/pause");
}

function restartClicked()
{
    send("/timer/restart");
}

function resumeClicked()
{
    send("/timer/resume");
}

function send(path)
{
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", path, true);
    xhttp.send();
}