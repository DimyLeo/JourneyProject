import {
  createBrowserRouter,
  RouterProvider
} from 'react-router-dom';
import { CreateTripPage } from './pages/create-trip';
import { TripDetailsPage } from './pages/trip-details';

const router = createBrowserRouter([
  {
    path: "/",
    element: <CreateTripPage />,
    errorElement: <h1>Pagina não encontrada.</h1>
  },
  {
    path: "/trips/:tripId",
    element: <TripDetailsPage />
  }
]);

export function App() {
  return <RouterProvider router={router} />
}