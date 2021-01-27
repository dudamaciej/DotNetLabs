"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/counterHub").build();

connection.on("updateCount", function (Count) {
    var text = document.getElementById("userOnlineBox");
    text.innerHTML = `Online Users: ${Count}`;
});

connection.start();