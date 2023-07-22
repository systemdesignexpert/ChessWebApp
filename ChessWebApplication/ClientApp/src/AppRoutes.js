import { ChessGame } from "./components/ChessGame";
import { ChessGrid } from "./components/ChessGrid";
import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <ChessGame />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/chess-game',
    element: <ChessGame />
    },
    {
        path: '/chess-grid',
        element: <ChessGrid />
    }
];

export default AppRoutes;
