"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/test-chathub").build();
var btnSend = document.getElementById("btnSend");
btnSend.disabled = true;

connection.on("MessageReceive", function (username, message) {
    var listItem = document.createElement("li");
    document.getElementById("messageList").appendChild(listItem);
    listItem.textContent = `${username}: ${message}`;
});

connection.start().then(function () {
    btnSend.disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

btnSend.addEventListener("click", function (event) {
    var username = document.getElementById("username").value;
    var message = document.getElementById("message").value;
    connection.invoke("NotifyToOther", username, message).catch(function (err) {
        return console.error(err.toString());
    });
})

