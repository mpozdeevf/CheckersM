"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

document.getElementById("startButton").disabled = true;

connection.on("Start", function (json) {
    var obj = JSON.parse(json);
    drawBoard(obj.Board, obj.PossiblePositions);
});

connection.on("End", function (message) {
    endOfTheGame(message)
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

function drawBoard(board, positions) {
    clearBoard();
    drawCheckers(board);
    play(positions);
}

function drawClientBoard(board) {
    clearBoard();
    drawCheckers(board);
}

function clearBoard() {
    for (var i = 0; i < 64; i++) {
        var id = Math.floor(i / 8).toString() + (i % 8).toString();
        var e = document.getElementById(id);
        e.innerText = "";
    }
}

function drawCheckers(str) {
    for (var i = 0; i < str.length; i++) {
        if (str[i] !== "e") {
            var id = Math.floor(i / 8).toString() + (i % 8).toString();
            var e = document.getElementById(id);
            e.innerText = str[i];
        }
    }
}

function play(positions) {
    var n = Math.floor(Math.random() * positions.length);
    var board = positions[n][positions[n].length - 1];
    setTimeout(drawClientBoard, 2000, positions[n][positions[n].length - 1]);
    //drawClientBoard(positions[n][positions[n].length - 1]);
    // setTimeout(connection.invoke('PlayGame', board).catch(function (err) {
    //         return console.error(err.toString());
    //     }),
    //     1000);
    connection.invoke("PlayGame", board).catch(function (err) {
       return console.error(err.toString());
    });
}

function endOfTheGame(message) {
    alert(message)
}