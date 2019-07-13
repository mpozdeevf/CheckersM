"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

var counter = 0;

document.getElementById("startButton").disabled = true;

connection.on("Start", function (json) {
    var obj = JSON.parse(json);
    //counter++;
    setTimeout(drawBoard, 1500, obj.Position, obj.PossiblePositions);
    //drawBoard(obj.Position, obj.PossiblePositions);
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

function drawBoard(position, positions) {
    for (var i = 0; i < position.length; i++) {
        setTimeout(drawCheckers, counter * 500, position[i]);
        counter++;
        //drawCheckers(position[i]);
    }
    play(positions);
}

function drawClientBoard(position, board) {
    for (var i = 0; i < position.length; i++) {
        setTimeout(drawCheckers, counter * 500, position[i]);
        counter++;
        //drawCheckers(position[i]);
    }
    connection.invoke("PlayGame", board).catch(function (err) {
        return console.error(err.toString());
    });
}

function clearBoard() {
    for (var i = 0; i < 64; i++) {
        var id = Math.floor(i / 8).toString() + (i % 8).toString();
        var e = document.getElementById(id);
        e.innerText = "";
    }
}

function drawCheckers(str) {
    clearBoard();
    for (var i = 0; i < str.length; i++) {
        if (str[i] !== "e") {
            var id = Math.floor(i / 8).toString() + (i % 8).toString();
            var e = document.getElementById(id);
            if (str[i] === 'w' || str[i] === 'W') {
                e.style.color = 'yellow';
            } else {
                e.style.color = 'black';
            }
            e.innerText = str[i];
        }
    }
}

function play(positions) {
    var n = Math.floor(Math.random() * positions.length);
    var board = positions[n][positions[n].length - 1];
    //counter++;
    setTimeout(drawClientBoard, 1500, positions[n], board);
}

function endOfTheGame(message) {
    alert(message)
}

function sleep(seconds) {
    setTimeout(function () {
        var a = 0;
    }, seconds);
}