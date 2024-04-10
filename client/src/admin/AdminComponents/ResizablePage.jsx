import React, { useEffect, useState } from "react";
import {
  ResizableHandle,
  ResizablePanel,
  ResizablePanelGroup,
} from "../../components/ui/resizable";
import Sidebar from "./Sidebar";
import { Outlet, useLocation } from "react-router-dom";

const ResizablePage = () => {
  const [loaded, setLoaded] = useState(false);
  const currentLocation = useLocation();
  console.log(currentLocation?.pathname, loaded);

  useEffect(() => {
    setLoaded(false);
    setTimeout(() => {
      setLoaded(true);
    }, 500);
  }, [currentLocation?.pathname]);

  return (
    <ResizablePanelGroup
      direction="horizontal"
      className="min-h-[100vh] max-w-[100vw]"
    >
      <ResizablePanel
        defaultSize={15}
        minSize={4}
        maxSize={15}
        className="bg-black text-white"
      >
        <Sidebar />
      </ResizablePanel>
      <ResizableHandle withHandle />
      <ResizablePanel
        defaultSize={85}
        className={`${
          loaded ? "opacity-100 mt-0 duration-1000" : "opacity-0 mt-10"
        }`}
      >
        <Outlet />
      </ResizablePanel>
    </ResizablePanelGroup>
  );
};

export default ResizablePage;
