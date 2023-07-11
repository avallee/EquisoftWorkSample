import GameList from "./components/GameList";
import GameView from "./components/GameView";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/games-list',
    element: <GameList />
  },
  {
      path: '/game/:playerid/:gameid',
      element: <GameView />
  }
];

export default AppRoutes;
