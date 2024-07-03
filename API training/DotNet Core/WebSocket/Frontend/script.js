document.addEventListener("DOMContentLoaded", function () {
    const chatBox = document.getElementById("chat-box");
    const messageInput = document.getElementById("message-input");
    const sendButton = document.getElementById("send-button");

    const wsUri = "wss://localhost:7115/api/websocket"; // Replace with your WebSocket URL

    let websocket = new WebSocket(wsUri);

    websocket.onopen = function (event) {
        console.log("WebSocket connection opened");
    };

    websocket.onmessage = function (event) {
        const message = event.data;
        appendMessage(message);
    };

    websocket.onclose = function (event) {
        console.log("WebSocket connection closed");
    };

    sendButton.addEventListener("click", function () {
        const message = messageInput.value.trim();
        if (message !== "") {
            websocket.send(message);
            messageInput.value = "";
        }
    });

    function appendMessage(message) {
        const p = document.createElement("p");
        p.textContent = message;
        chatBox.appendChild(p);
    }
});
