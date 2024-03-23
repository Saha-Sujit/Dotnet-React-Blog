import React from "react";
import { Link } from "react-router-dom";

const Sidebar = () => {
  return (
    <>
      <Link to={"/dashboard/posts"}>Posts</Link>
      <Link to={"/dashboard/pages"}>Pages</Link>
      <Link to={"/dashboard/media"}>Media</Link>
    </>
  );
};

export default Sidebar;
