import dashboardIcon from "../assets/icons/dashboard.svg";
import postsIcon from "../assets/icons/posts.svg";
import pagesIcon from "../assets/icons/pages.svg";
import mediaIcon from "../assets/icons/media.svg";

const sideBarMenus = [
  {
    menuName: "Dashboard",
    menuLink: "/dashboard",
    menuIcon: dashboardIcon,
  },
  {
    menuName: "Posts",
    menuLink: "/dashboard/posts",
    menuIcon: postsIcon,
  },
  {
    menuName: "Pages",
    menuLink: "/dashboard/pages",
    menuIcon: pagesIcon,
  },
  {
    menuName: "Media",
    menuLink: "/dashboard/media",
    menuIcon: mediaIcon,
  },
];

export default sideBarMenus;
