"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/game").build();

var isUserTurn = false;

var possiblePositions;
var currentPosition;

document.getElementById("startButton").disabled = true;

connection.on("Start", function (json) {
    var obj = JSON.parse(json);
    possiblePositions = obj.PossiblePositions;
    currentPosition = obj.Position;
    setTimeout(drawBoard, 0);
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
    var counter = 0;
    for (var i = 0; i < currentPosition.length; i++) {
        setTimeout(drawCheckers, counter * 600, currentPosition[i]);
        counter++;
    }
    setTimeout(play, counter * 600);
}

function drawClientBoard(board) {
    drawCheckers(board);
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
    isUserTurn = true;
}

function endOfTheGame(message) {
    alert(message);
}

var newPossiblePositions;
var isMoveCheckerChosen = false;
var index;
var isInTurn;

function cellOnClick(e) {
    var cell = e.target;
    if (isUserTurn) {
        brushCheckers();
        index = 0;
        isInTurn = false;
        cell.style.color = 'orange';
        cell.style.backgroundColor = 'yellow';
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
            if (newPossiblePositions[i][index][id] === 'w' || newPossiblePositions[i][index][id] === 'W') {
                tempPossiblePositions.push(newPossiblePositions[i]);
            }
        }
        if (tempPossiblePositions.length === 0) {
            if (!isInTurn) {
                isUserTurn = true;
            }
            isMoveCheckerChosen = true;
        } else if (tempPossiblePositions[0].length - index === 1) {
            drawClientBoard(tempPossiblePositions[0][index]);
            brushCheckers();
        } else {
            isMoveCheckerChosen = true;
            isInTurn = true;
            newPossiblePositions = tempPossiblePositions;
            drawCheckers(tempPossiblePositions[0][index]);
            index++;
            brushCheckers();
            cell.style.color = 'orange';
            cell.style.backgroundColor = 'yellow';
        }
    }
}

function brushCheckers() {
    for (var i = 0; i < 64; i++) {
        var id = Math.floor(i / 8).toString() + (i % 8).toString();
        var e = document.getElementById(id);
        if (e.innerText === 'w' || e.innerText === 'W') {
            e.onclick = cellOnClick;
            e.style.color = 'yellow';
        } else if (e.innerText === 'b' || e.innerText === 'B') {
            e.style.color = 'black';
        }
        if (e.style.backgroundColor === 'yellow') {
            e.style.backgroundColor = 'chocolate';
        }
    }
}