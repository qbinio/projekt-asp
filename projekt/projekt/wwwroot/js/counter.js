"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/counterHub").build();
connection.on("updateCount", (userCount) => {
    let textMessage = document.getElementById("countUsers");
    let count = document.createTextNode(userCount);
    textMessage.appendChild(count)
});
connection.start();