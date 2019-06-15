"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

document.getElementById("startButton").disabled = true;

connection.on("Start", function (json) {
    //var encodedMsg = json;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("messagesList").appendChild(li);
    drawBoard(json);
});

connection.start().then(function () {
    document.getElementById("startButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

var startButton = document.getElementById("startButton")
startButton.addEventListener("click", function (event) {
    connection.invoke("StartGame").catch(function (err) {
        return console.error(err.toString());
    });
    startButton.style.display = "none";
    event.preventDefault();
});

function drawBoard(json) {
    var obj = JSON.parse(json);
    var str = obj.BitBoard.WhiteCheckersStr;

    str = obj.BitBoard.BlackCheckersStr;
    drawCheckers(obj.BitBoard.WhiteCheckersStr, "w");
    drawCheckers(obj.BitBoard.BlackCheckersStr, "b");
    drawCheckers(obj.BitBoard.WhiteKingsStr, "W");
    drawCheckers(obj.BitBoard.BlackKingsStr, "B");
}

function drawCheckers(str, letter) {
    for (var i = 0; i < str.length; i++) {
        if (str[i] === "1") {
            var id = Math.floor(i / 8).toString() + (i % 8).toString();
            var e = document.getElementById(id);
            e.innerText = letter;
        }
    }
}