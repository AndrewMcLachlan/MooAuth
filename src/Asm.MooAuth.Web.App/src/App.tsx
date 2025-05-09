import "./App.css";
//import * as Icons from "./assets";

import React from "react";
//import { Link } from "react-router-dom";

import { MooAppLayout, NavItem } from "@andrewmclachlan/mooapp";
import { useIsAuthenticated } from "@azure/msal-react";
import { Application, Dashboard, User, Users, UserShield } from "@andrewmclachlan/mooicons";
//import { useHasRole } from "hooks";

const App: React.FC = () => {

    const isAuthenticated = useIsAuthenticated();

    //const hasRole = useHasRole();

    if (!isAuthenticated) return null;

    /*const menu = hasRole("Admin") ?
        [(<Link key="settings" to="/settings" aria-label="Settings"><Icons.Cog /></Link>)] :
        [];*/

    return (
        <MooAppLayout
            header={{ menu: [], userMenu: userMenu }}
            sidebar={{ navItems: sideMenu }}
        />
    );
};

export default App;

const userMenu: NavItem[] = [
    {
        text: "Profile",
        route: "/profile",
    }
];

const sideMenu: NavItem[] = [
    {
        text: "Dashboard",
        route: "/",
        image: <Dashboard />
    },
    {
        text: "Applications",
        route: "/applications",
        image: <Application />
    },
    {
        text: "Roles",
        route: "/roles",
        image: <UserShield />

    },
    {
        text: "Users",
        route: "/users",
        image: <User />
    },
    {
        text: "Groups",
        route: "/groups",
        image: <Users />
    },
    {
        text: "Connectors",
        route: "/connectors"
    },
];
