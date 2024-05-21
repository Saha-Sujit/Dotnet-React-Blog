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
import ResizablePage from "./admin/AdminComponents/ResizablePage";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import Auth from "./admin/Pages/Auth";

const queryClient = new QueryClient();

const router = createBrowserRouter([
  {
    path: "/",
    element: <>Home Page</>,
    errorElement: <Error />,
  },
  {
    path: "/auth",
    children: [
      {
        path: "",
        element: <Auth />,
      },
      {
        path: "login",
        element: <Auth />,
      },
      {
        path: "register",
        element: <Auth />,
      },
    ],
  },
  {
    path: "/dashboard",
    element: (
      <>
        <Header />
        <ResizablePage />
      </>
    ),
    children: [
      {
        path: "",
        element: <Dashboard />,
      },
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
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router} />
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  </React.StrictMode>
);
