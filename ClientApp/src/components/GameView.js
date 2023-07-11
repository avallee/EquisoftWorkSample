import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
export default function GameView() {

    const { playerid, gameid } = useParams();

    const [ gameState, setGameState ] = useState({ loaded: false, game: null, selected: null, interval: null });
    const [ moveState, setMoveState ] = useState({ loaded: false, moves: null, selected: null});

    useEffect(() => {
        if (moveState.loaded === false) {
            setMoveState((prev)=>({ ...prev, loaded: 'loading' }));
            fetch('/api/moves').then(resp => resp.json()).then((moves) => {
                setMoveState({ loaded: 'loaded', moves, selected: moves[0] });

            });
        }
    }, [moveState]);

    useEffect(() => {
        function load(pred) {
            if (pred()) {
                setGameState((prev) => ({ ...prev, loaded: 'loading' }));
                fetch(`/api/game/${gameid}`).then(resp => resp.json()).then((game) => {
                    setGameState((prev)=>({...prev, loaded: 'loaded', game }));
                });
            }
        };
        load(()=> { return gameState.loaded === false });
        const intervalId = setInterval(load, 15 * 1000, () => { return gameState.loaded !== 'loading' });
        return () => clearInterval(intervalId);

    }, [gameState, gameid]);

    function renderMovesOptions() {
        return moveState.loaded === 'loaded' ?
            (<select onChange={e => setMoveState((prev)=>({...prev, selected: e.target.value }))}>
                {moveState.moves.map((move, index) => {
                    return <option key={index}>{move}</option>
                })}
            </select>) :
            (<select>
                <option>
                    Moves
                </option>
            </select>)
    }

    function renderButtons() {
        return (<>
                <button onClick={SendMovePlayer}>Play</button>
                {playerid == 1 && <button onClick={SendMoveComputer}>Play Computer</button>})
            </>);
    }

    function renderGame() {
        return gameState.loaded === 'loaded' || (gameState.loaded === 'loading' && gameState.game !== null) ?
            (<>
                {gameState.game && gameState.game.winner !== null && <h2>Player {gameState.game.winner} has won!</h2>}
                <table>
                    <thead>
                        <tr>
                            <th colSpan="2">Match results</th>
                        </tr>
                        <tr>
                            <th>Player 1 Wins</th>
                            <th>Player 2 Wins</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{gameState.game.player1Wins}</td>
                            <td>{gameState.game.player2Wins}</td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <thead>
                        <tr>
                            <th colSpan="4">Current Game</th>
                        </tr>
                        <tr>
                            <th>Player 1 Last Move</th>
                            <th>Player 2 Last Move</th>
                            <th>Your current move</th>
                            <th>Who has played</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{gameState.game.player1LastMove}</td>
                            <td>{gameState.game.player2LastMove}</td>
                            <td>{gameState.selected}</td>
                            <td>{gameState.game.state}</td>
                        </tr>
                    </tbody>
                </table>
            </>) :( <p>loading...</p>) 


    }

    async function SendMove(computer) {
        if (moveState.loaded !== 'loaded') return;

        await fetch(`/api/game/${gameid}`, {
            method: 'post',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify({ move: moveState.selected, player: parseInt(playerid), computer })
        });

        setGameState((prev) => ({...prev, selected: moveState.selected }));
        // No need to handle result, game auto-reloads.
    }

    function SendMovePlayer() {
        SendMove(false);
    }
    function SendMoveComputer() {
        SendMove(true);
    }

    return (
        <>
            <h1>Rock, Paper, Scissors Game: {gameid}</h1>
            <h2>You are player {playerid}</h2>

            {renderGame()}

            {renderMovesOptions()}
            {(gameState.game && gameState.game.winner === null) && renderButtons()}
        </>
    )
}