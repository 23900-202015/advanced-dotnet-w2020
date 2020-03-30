// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var userName;
var roomName;
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on('ReceiveMessage', displayMessage);
connection.onclose(sayGoodbye);

connection.start();

var msgForm = document.forms.msgForm;
msgForm.addEventListener('submit', function (e) {
    e.preventDefault();
    var userMessage = document.getElementById('userMessage');
    var text = userMessage.value;
    userMessage.value = '';
    userName = document.getElementById('username').value;
    roomName = document.getElementById('roomName').value;
    console.log(roomName);
    sendMessage(roomName,userName, text);
});


function sendMessage(roomName, userName, message) {
    if (message && message.length) {
        connection.invoke('SendMessage', roomName, userName, message);
    }
}

function displayMessage(name, time, message) {

    var specialClass = userName === name ? "sender" : "recipient";
    switch (name) {
        case "Chat Hub":
            specialClass = "systemUser";
            break;
        case userName:
            specialClass = "sender";
            break;
        default:
            specialClass = "recipient";
    }

    var friendlyTime = moment(time).format('H:mm:ss');


    var userLi = document.createElement('li');
    userLi.className = 'userLi list-group-item ' + specialClass;
    userLi.textContent = friendlyTime + ", " + name + " says:";

    var MessageLi = document.createElement('li');
    MessageLi.className = 'messageLi list-group-item pl-5 ' + specialClass;
    MessageLi.textContent = message;

    var chatHistoryUl = document.getElementById('chatHistory');
    chatHistoryUl.appendChild(userLi);
    chatHistoryUl.appendChild(MessageLi);

    //scroll to the newest message
    $("#chatHistory").animate({ scrollTop: $('#chatHistory').prop('scrollHeight') }, 50);
}

function sayGoodbye() {
    connection.invoke(userName);

}

document.getElementById("btnJoin").addEventListener('click', function (e) {
    e.preventDefault();
    var roomName = document.getElementById('roomName').value;

    if (roomName && roomName.length) {
        document.getElementById("btnJoin").disabled = true;
        connection.invoke('JoinRoom', roomName);
    }
});