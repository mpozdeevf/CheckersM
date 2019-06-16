"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

document.getElementById("startButton").disabled = true;

connection.on("Start", function (json) {
    var obj = JSON.parse(json);
    drawBoard(obj.BitBoard, obj.PossiblePositions);
});

connection.start().then(function () {
    document.getElementById("startButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

var startButton = document.getElementById("startButton");
startButton.addEventListener("click", function (event) {
    connection.invoke("StartGame").catch(function (err) {
        return console.error(err.toString());
    });
    startButton.style.display = "none";
    event.preventDefault();
});

function drawBoard(bitBoard, positions) {
    clearBoard();
    drawCheckers(bitBoard.WhiteCheckersStr, "w");
    drawCheckers(bitBoard.BlackCheckersStr, "b");
    drawCheckers(bitBoard.WhiteKingsStr, "W");
    drawCheckers(bitBoard.BlackKingsStr, "B");
    play(positions);
}

function drawClientBoard(bitBoard) {
    clearBoard();
    drawCheckers(bitBoard.WhiteCheckersStr, "w");
    drawCheckers(bitBoard.BlackCheckersStr, "b");
    drawCheckers(bitBoard.WhiteKingsStr, "W");
    drawCheckers(bitBoard.BlackKingsStr, "B");
}

function clearBoard() {
    for (var i = 0; i < 64; i++) {
        var id = Math.floor(i / 8).toString() + (i % 8).toString();
        var e = document.getElementById(id);
        e.innerText = "";
    }
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

function play(positions) {
    var n = Math.floor(Math.random() * positions.length);
    var newJson = JSON.stringify(positions[n][positions[n].length - 1]);
    setTimeout(drawClientBoard, 2000, positions[n][positions[n].length - 1]);
    //drawClientBoard(positions[n][positions[n].length - 1]);
    setTimeout(connection.invoke("PlayGame", newJson).catch(function(err) {
            return console.error(err.toString());
        }),
        1000);
    //connection.invoke("PlayGame", newJson).catch(function (err) {
    //    return console.error(err.toString());
    //});
}