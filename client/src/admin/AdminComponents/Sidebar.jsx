import sideBarMenus from "../../lib/sidebarMenu";
import React from "react";
import { NavLink } from "react-router-dom";
// import dashboardIcon from "../../assets/icons/dashboard.svg";

const Sidebar = () => {
  return (
    <div className="flex flex-col">
      {sideBarMenus.map(({ menuName, menuLink, menuIcon }, index) => (
        <NavLink
          to={menuLink}
          end
          className={({ isActive }) =>
            `p-2 ${isActive ? "bg-primary" : "hover:bg-slate-800"}`
          }
          key={index}
        >
          <span className="flex gap-3">
            <img src={menuIcon} alt={menuName} width="13" height="13" />
            {menuName}
          </span>
        </NavLink>
      ))}
    </div>
  );
};

export default Sidebar;
