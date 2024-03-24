import sideBarMenus from "../../lib/sidebarMenu";
import React from "react";
import { NavLink } from "react-router-dom";

const Sidebar = () => {
  return (
    <div className="flex flex-col">
      {sideBarMenus.map(({ menuName, linkTo }, index) => (
        <NavLink
          to={linkTo}
          end
          className={({ isActive }) =>
            `p-2 ${isActive ? "bg-primary" : "hover:bg-slate-800"}`
          }
          key={index}
        >
          {menuName}
        </NavLink>
      ))}
    </div>
  );
};

export default Sidebar;
