import { useEffect, useState } from 'react';

export default function GameList() {
    const [games, setGames] = useState(null);
    const [loading, setLoading] = useState(null);

    useEffect(() => {
        if (loading == null) {
            setLoading('loading');
            fetch('/api/games').then(resp => resp.json()).then((g) => {
                setGames(g);
                setLoading('loaded');
            });
        }
    }, [games, loading]);

    function NewGame() {
        fetch('/api/games', { method: 'post' }).then(() => {
            setLoading(null);
        });
    }

    function renderGames(gamesList) {
        return (<ol>
            {gamesList.map(game => {
                return <li key={game.id}>
                    Created: {game.created}{game.winningPlayer ? ` Won by player ${game.winningPlayer}` : ""} 
                    &ensp;<a href={`/game/1/${game.id}`}>View Player 1</a>
                    &ensp;<a href={`/game/2/${game.id}`}>View Player 2</a>
                </li>
            })}
        </ol>)
    }

    let renderedGames = loading === 'loaded' ? renderGames(games) : <p>Loading...</p>;
    return (
            <>
            <h1>Rock, Paper, Scissors games</h1>
            {renderedGames}
            <button onClick={NewGame}>
                New Game
            </button>
            </>
    );
}