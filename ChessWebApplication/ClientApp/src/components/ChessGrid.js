import React, { Component } from 'react';
import blackKing from './../ChessPieceImages/black/king.svg';
import blackQueen from './../ChessPieceImages/black/queen.svg';
import blackPawn from './../ChessPieceImages/black/pawn.svg';
import blackBishop from './../ChessPieceImages/black/bishop.svg';
import blackKnight from './../ChessPieceImages/black/knight.svg';
import blackRook from './../ChessPieceImages/black/rook.svg';

import whiteKing from './../ChessPieceImages/white/king.svg';
import whiteQueen from './../ChessPieceImages/white/queen.svg';
import whitePawn from './../ChessPieceImages/white/pawn.svg';
import whiteBishop from './../ChessPieceImages/white/bishop.svg';
import whiteKnight from './../ChessPieceImages/white/knight.svg';
import whiteRook from './../ChessPieceImages/white/rook.svg';

import successSound from './../ChessPieceImages/success.wav';
import winSound from './../ChessPieceImages/win.wav';
import errorSound from './../ChessPieceImages/error.wav';

import './ChessGrid.css';

export class ChessGrid extends Component {
    static displayName = ChessGrid.name;

    constructor(props) {
        super(props);
        this.doMove = this.doMove.bind(this);
        this.getPos = this.getPos.bind(this);
        this.SoundPlayer = this.SoundPlayer.bind(this);
        this.state = {
            squares: props.squares,
            loading: true,
            startingPos: "",
            endingPos: "",
            gameId: props.gameId,
            moveSuccess: null,
            gameOver: false
        };
    }

    getPos(i, j){
        var first = "a".charCodeAt(0) + i;
        var second = "1".charCodeAt(0) + j;
        var s = "";
        s += String.fromCharCode(first);
        s += String.fromCharCode(second);
        return s;
    }

    async handleClick(i, j) {
        if (!this.state.startingPos) {
            this.setState({ endingPos: "", startingPos: this.getPos(i, j) });
            return;
        }
        if (!this.state.endingPos) {
            this.state.endingPos = this.getPos(i, j);
            this.setState({ endingPos: this.getPos(i, j)});
            const result = await this.doMove();
            this.setState({ endingPos: "", startingPos: "" });
            return;
        }
        
        console.log(`Cell clicked at (${i}, ${j})`);
        // Add your custom logic here
    };

    async doMove() {

        const gameId = this.state.gameId;
        const move_url = 'ChessGame/move?gameId='
            + gameId
            + "&startPos="
            + this.state.startingPos
            + "&endPos="
            + this.state.endingPos;

        const response = await fetch(move_url, {
            method: 'post',
            headers: { 'Content-Type': 'application/json' }
        });
        const moveResult = await response.json();
        // display move success/failure
        this.setState({
            moveSuccess: moveResult.isSuccess,
            gameOver: moveResult.isGameOver,
            squares: moveResult.listOfSquares,
            loading: false
        });
        this.props.handleOnMove();

        //const url = 'ChessGame/getGame?id=' + gameId;
        //const result = await fetch(url);
        //const list_of_squares = await result.json();
        //this.setState({ squares: list_of_squares, loading: false });

        // Add your custom logic here
    };

    componentDidUpdate(prevProps) {
        this.SoundPlayer();
        
        if (prevProps.squares !== this.props.squares) {
            this.setState({
                squares: this.props.squares,
                loading: true,
                gameId: this.props.gameId
            });
        }

    }

    SoundPlayer = () => {
        if (this.state.moveSuccess === null) return;
        

        this.setState({ moveSuccess: null });

        const successElement = new Audio(successSound);
        const errorElement = new Audio(errorSound);
        const winElement = new Audio(winSound);
        if (this.state.moveSuccess === true) successElement.play();
        if (this.state.moveSuccess === false) errorElement.play();

        if (this.state.gameOver) {
            winElement.play();
        }

    }

    render() {
        
        const squares = this.state.squares;
        const gridSize = 8;
        const chessCells = [];
        

        // Generate the chess cells
        for (let i = 0; i < gridSize; i++) {
            for (let j = 0; j < gridSize; j++) {
                //console.log(squares);
                var text = "";
                var pieceImage = null;
                if (squares) {
                    const sq = squares.filter(x => x.x == j && x.y == gridSize - i - 1)
                    if (sq.length > 0 && sq[0].piece != null) {
                        const pc = sq[0].piece.color;
                        const pn = sq[0].piece.name;
                        if (pc == "WHITE") {
                            if (pn == "ROOK") {
                                pieceImage = whiteRook;
                            } else if (pn == "KNIGHT") {
                                pieceImage = whiteKnight;
                            } else if (pn == "QUEEN") {
                                pieceImage = whiteQueen;
                            } else if (pn == "KING") {
                                pieceImage = whiteKing;
                            } else if (pn == "PAWN") {
                                pieceImage = whitePawn
                            } else if (pn == "BISHOP") {
                                pieceImage = whiteBishop;
                            }
                        } else {
                            if (pn == "ROOK") {
                                pieceImage = blackRook;
                            } else if (pn == "KNIGHT") {
                                pieceImage = blackKnight;
                            } else if (pn == "QUEEN") {
                                pieceImage = blackQueen;
                            } else if (pn == "KING") {
                                pieceImage = blackKing;
                            } else if (pn == "PAWN") {
                                pieceImage = blackPawn
                            } else if (pn == "BISHOP") {
                                pieceImage = blackBishop;
                            }
                        }
                    }
                    
                }
                
                var cellColor = (i + j) % 2 === 0 ? '#bfff00' : '#26734d';
                const selectedColor = '#f7ac81';

                if (
                    this.state.endingPos == this.getPos(j, gridSize - i - 1) ||
                    this.state.startingPos == this.getPos(j, gridSize - i - 1)
                ) {
                    cellColor = selectedColor;
                }

                const cellKey = `${j} - ${gridSize - i - 1}`;

                const cellStyle = {
                    backgroundColor: cellColor,
                    width: '100px',
                    height: '100px',
                };

                const imageStyle = {
                    width: "100%",
                    height: "100%",
                    objectFit: "contain",
                };

                if (pieceImage) {
                    chessCells.push(
                        <div
                            key={cellKey}
                            style={cellStyle}
                            className="chess-cell"
                            onClick={() => this.handleClick(j, gridSize - i - 1)}
                        >
                            <img src={pieceImage} alt="" style={imageStyle} />
                        </div>
                    );
                } else {
                    chessCells.push(
                        <div
                            key={cellKey}
                            style={cellStyle}
                            className="chess-cell"
                            onClick={() => this.handleClick(j, gridSize - i - 1)}
                        />
                    );
                }
            }
        }

        return (
            <div className="centered-text">
                <h1>Single Player Chess</h1>
                <div className="chess-grid">
                    {chessCells}
                </div>
                <h1> Single Player Chess </h1>
            </div>
        );
    
    }
}