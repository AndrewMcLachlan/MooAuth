import "./App.css";

import React from "react";

import { MooAppLayout } from "@andrewmclachlan/moo-app";
import { NavItem } from "@andrewmclachlan/moo-ds";
import { useIsAuthenticated } from "@azure/msal-react";
import { Application, Dashboard, Database, User, Users, UserShield } from "@andrewmclachlan/moo-icons";

const Layout: React.FC = () => {

    const isAuthenticated = useIsAuthenticated();

    if (!isAuthenticated) return null;

    return (
        <MooAppLayout
            header={{ menu: [], userMenu: userMenu }}
            sidebar={{ navItems: sideMenu }}
        />
    );
};

export default Layout;

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
        icon: Dashboard
    },
    {
        text: "Applications",
        route: "/applications",
        icon: Application
    },
    {
        text: "Roles",
        route: "/roles",
        icon: UserShield

    },
    {
        text: "Users",
        route: "/users",
        icon: User
    },
    {
        text: "Groups",
        route: "/groups",
        icon: Users
    },
    {
        text: "Connectors",
        route: "/connectors"
    },
    {
        text: "Data Sources",
        route: "/datasources",
        icon: Database
    },
];
