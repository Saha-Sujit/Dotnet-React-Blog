import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Dashboard from "./admin/Pages/Dashboard";
import Header from "./admin/AdminComponents/Header";
import Error from "./admin/Pages/Error";
import Posts from "./admin/Pages/Posts";
import Pages from "./admin/Pages/Pages";
import Media from "./admin/Pages/Media";

const router = createBrowserRouter([
  {
    path: "/",
    element: <>Home Page</>,
    errorElement: <Error />,
  },
  {
    path: "/dashboard",
    element: (
      <>
        <Header />
        <Dashboard />
      </>
    ),
    children: [
      {
        path: "posts",
        element: <Posts />,
      },
      {
        path: "pages",
        element: <Pages />,
      },
      {
        path: "media",
        element: <Media />,
      },
    ],
  },
  {
    path: "about",
    element: <div>About</div>,
  },
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
