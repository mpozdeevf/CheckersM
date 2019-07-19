"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

var counter = 0;

var isUserTurn = false;

var possiblePositions;
var currentPosition;

document.getElementById("startButton").disabled = true;

connection.on("Start", function (json) {
    var obj = JSON.parse(json);
    possiblePositions = obj.PossiblePositions;
    currentPosition = obj.Position;
    setTimeout(drawBoard, 500);
});

connection.on("End", function (message) {
    endOfTheGame(message);
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

function drawBoard() {
    for (var i = 0; i < currentPosition.length; i++) {
        setTimeout(drawCheckers, 0, currentPosition[i]);
        counter++;
    }
    play();
}

function drawClientBoard(position, board) {
    for (var i = 0; i < position.length; i++) {
        drawCheckers(position[i]);
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
        if (str[i] === "e") {
            var id1 = Math.floor(i / 8).toString() + (i % 8).toString();
            var e1 = document.getElementById(id1);
            e1.onclick = cellMoveOnClick;
        }
        if (str[i] !== "e") {
            var id = Math.floor(i / 8).toString() + (i % 8).toString();
            var e = document.getElementById(id);
            if (str[i] === 'w' || str[i] === 'W') {
                e.onclick = cellOnClick;
                e.style.color = 'yellow';
            } else {
                e.style.color = 'black';
            }
            e.innerText = str[i];
        }
    }
}

function play() {
    // var n = Math.floor(Math.random() * possiblePositions.length);
    // var board = possiblePositions[n][possiblePositions[n].length - 1];
    isUserTurn = true;
    //setTimeout(drawClientBoard, 1500, possiblePositions[n], board);
}

function endOfTheGame(message) {
    alert(message);
}

var newPossiblePositions;
var isMoveCheckerChosen = false;

function cellOnClick(e) {
    var cell = e.target;
    if (isUserTurn) {
        cell.style.color = 'orange';
        newPossiblePositions = [];
        var id = parseInt(cell.id[0]) * 8 + parseInt(cell.id[1]);
        for (var i = 0; i < possiblePositions.length; i++) {
            if (possiblePositions[i][0][id] === 'e') {
                newPossiblePositions.push(possiblePositions[i]);
            }
        }
        isMoveCheckerChosen = true;
    }
}

var tempPossiblePositions;

function cellMoveOnClick(e) {
    var cell = e.target;
    var id = parseInt(cell.id[0]) * 8 + parseInt(cell.id[1]);
    if (isMoveCheckerChosen) {
        isMoveCheckerChosen = false;
        isUserTurn = false;
        tempPossiblePositions = [];
        for (var i = 0; i < newPossiblePositions.length; i++) {
            if (newPossiblePositions[i][0][id] === 'w' || newPossiblePositions[i][0][id] === 'W') {
                tempPossiblePositions.push(newPossiblePositions[i]);
            }
        }
        if (tempPossiblePositions.length === 0) {
            alert("Move is impossible");
        } //else if (tempPossiblePositions[0].length === 1) {
        //     drawCheckers(tempPossiblePositions[0][0]);
        // } else {
        //     dr
        // }
        var board = tempPossiblePositions[0][tempPossiblePositions[0].length - 1];
        drawClientBoard(tempPossiblePositions[0], board)
    }
}