import React, { Component } from 'react';
import { ChessGrid } from './ChessGrid';
import './ChessGrid.css';
import './ChessGame.css';
import initializeSound from './../ChessPieceImages/initialize.wav';

export class ChessGame extends Component {
    static displayName = ChessGame.name;

  constructor(props) {
    super(props);
    this.state = { squares: [], initialize: false, gameId: null, moveList: []};
      this.createNewGame = this.createNewGame.bind(this);
      this.getMoves = this.getMoves.bind(this);
  }

    handleClick = () => {
        //this.setState({ moveList: "moved now.!!" });
        console.log('Button clicked!');
        this.getMoves();
        // Add your custom logic here
    };

    async getMoves() {
        const url = 'ChessGame/getMoveList?id=' + this.state.gameId;
        const result = await fetch(url);
        const list_of_moves = await result.json();
        //return list_of_moves;
        this.setState({ moveList: list_of_moves });
        // Add your custom logic here
    };

    SoundPlayer = () => {
        if (!this.state.initialize) return;

        this.setState({ initialize: false });
        const initializeElement = new Audio(initializeSound);
        initializeElement.play();

    }

    async createNewGame() {
        const response = await fetch('ChessGame/newGame', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' }
        });
        const data = await response.json();
        const gameId = data.id;
        const url = 'ChessGame/getGame?id=' + gameId;
        const result = await fetch(url);
        const list_of_squares = await result.json();
        this.setState({ squares: list_of_squares, initialize: true, gameId: gameId, moveList: []});
        // Add your custom logic here
    };

    getExistingGame = () => {
        console.log("getExistingGame");
        //const url = 'ChessGame/getGame/' + gameId;
        //const result = await fetch(url);
        //const list_of_squares = await result.json();
        //this.setState({ squares: list_of_squares, loading: false });
        // Add your custom logic here
    };

    componentDidMount() {
        this.populateWeatherData();
        //fetch('WeatherForecast/Get2123').then(
        //    val => val.json()
        //). then(
        //  data => {
        //    this.setState({forecasts: data, loading: false});
        //    console.log(this.state.forecasts);
        //  }
        //);

    }
  
  
  render() {

    this.SoundPlayer();
    return (
      <div>

            <div className="centered-div">
                <button className="centered-button" onClick={this.createNewGame}><h5>Start New Game</h5 ></button>
            </div>

            <div className="container">
                <div className="ChessGame">
                    <ChessGrid squares={this.state.squares} gameId={this.state.gameId} handleOnMove={this.handleClick} />
                </div>
                <div className="MoveList">
                    <h1>Game Moves</h1>
                    {this.state.moveList.map((item, index) => (
                        <div key={index}>{item.number}. {item.piece} {item.startPos} {item.endPos}</div>
                    ))}
                </div>
            </div>
      </div>
    );
  }

    async populateWeatherData() {
        const response = await fetch('WeatherForecast/Get2123');
        const data = await response.json();
        //this.setState({ forecasts: data, loading: false });
    }

}
