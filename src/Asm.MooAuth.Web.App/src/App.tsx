import "./App.scss";
//import * as Icons from "./assets";

import React from "react";
//import { Link } from "react-router-dom";

import { MooAppLayout, NavItem } from "@andrewmclachlan/mooapp";
import { useIsAuthenticated } from "@azure/msal-react";
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
            footer={{ copyrightYear: 2024 }}
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

const sideMenu = [
    {
        text: "Dashboard",
        route: "/"
    },
    {
        text: "Applications",
        route: "/applications"
    },
    {
        text: "Roles",
        route: "/roles"
    },
    {
        text: "Users",
        route: "/users"
    },
    {
        text: "Groups",
        route: "/groups"
    },
    {
        text: "Connectors",
        route: "/connectors"
    },
];
