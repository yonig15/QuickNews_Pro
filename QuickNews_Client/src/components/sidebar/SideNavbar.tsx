import React from "react";
import { Link } from "react-router-dom";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import "./SideNavbar.css";
import LogoutButton from "../auth0/LogoutBtn";
import LoginButton from "../auth0/LoginBtn";
import Profile from "../auth0/Profile";

import { useAuth0 } from "@auth0/auth0-react";

function SideNavbar(props: any) {
  const { isAuthenticated } = useAuth0();

  return (
    <div className="sidenavbar--container">
      <ul className="sidenavbar--menu">
        <br />
        {/* <li>
          <div className="sidenavbar--img--container">
            <img className="sidenavbar--img" src="https://www.nixapublicschools.net/cms/lib/MO01909223/Centricity/Domain/202/QuickNews%20Newsletter%20LOGO%20new.png" alt="logo" />
          </div>
        </li> */}
        <br />
        {!isAuthenticated && (
          <>
            <li>
              <LoginButton />
            </li>
            <li>login with the buttom above!</li>
          </>
        )}
        {isAuthenticated && (
          <>
            <li>
              <LogoutButton />
            </li>
            <li>
              <Profile />
            </li>
            <li>
              <Link to="/CuriousPage">
                <KeyboardArrowRightIcon />
                <label className="label--sidenavbar">Curious</label>
              </Link>
            </li>
            <li>
              <Link to="/DefinitionsPage">
                <KeyboardArrowRightIcon />
                <label className="label--sidenavbar">Definitions</label>
              </Link>
            </li>
            <li>
              <Link to="/HomePage">
                <KeyboardArrowRightIcon />
                <label className="label--sidenavbar">HomePage</label>
              </Link>
            </li>
            <li>
              <Link to="/PopularsPage">
                <KeyboardArrowRightIcon />
                <label className="label--sidenavbar">Populars</label>
              </Link>
            </li>
          </>
        )}
      </ul>
    </div>
  );
}

export default SideNavbar;
