import React from "react";
import {
  ResizableHandle,
  ResizablePanel,
  ResizablePanelGroup,
} from "../../components/ui/resizable";
import Sidebar from "./Sidebar";
import { Outlet } from "react-router-dom";

const ResizablePage = () => {
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
      <ResizablePanel defaultSize={85}>
        <Outlet />
      </ResizablePanel>
    </ResizablePanelGroup>
  );
};

export default ResizablePage;
