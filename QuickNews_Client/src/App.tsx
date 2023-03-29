import React from "react";
import "./App.css";
import { Routes, Route } from "react-router-dom";
import { CuriousPage, DefinitionsPage, HomePage, PopularsPage } from "./pages/PagesIndex";
import SideNavbar from "./components/sidebar/SideNavbar.jsx";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  return (
    <div className="App">
      <div>
        <div>
          <SideNavbar />
        </div>
        <div className="app--pageContent">
          <Routes>
            <Route path="/CuriousPage" element={<CuriousPage />}></Route>
            <Route path="/DefinitionsPage" element={<DefinitionsPage />}></Route>
            <Route path="/HomePage" element={<HomePage />}></Route>
            <Route path="/PopularsPage" element={<PopularsPage />}></Route>
          </Routes>
        </div>
      </div>
    </div>
  );
}

export default App;
