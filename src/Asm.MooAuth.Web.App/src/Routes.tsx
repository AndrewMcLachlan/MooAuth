import { RouteDefinition } from "@andrewmclachlan/mooapp";
import App from "App";
import * as Pages from "./pages";

export const routes: RouteDefinition = {
    layout: {
        path: "/", element: <App />, children: {
            home: { path: "/", element: <Pages.Dashboard /> },
            applications: { path: "/applications", element: <Pages.Applications />, },
            createApplication: { path: "/applications/create", element: <Pages.CreateApplication /> },
            application: {
                path: "/applications/:id", element: <Pages.Application />,
                children: {
                    details: { path: "details", element: <Pages.ApplicationDetails /> },
                }
            },

            roles: { path: "/roles", element: <Pages.Roles />, },
            createRole: { path: "/roles/create", element: <Pages.CreateRole /> },
            role: {
                path: "/roles/:id", element: <Pages.Role />,
                children: {
                    details: { path: "details", element: <Pages.RoleDetails /> },
                }
            },
            connectors: { path: "/connectors", element: <Pages.Connectors />, },
            createConnector: { path: "/connectors/create", element: <Pages.CreateConnector /> },
            createEntraConnector: { path: "/connectors/create/entra", element: <Pages.CreateEntra /> },
            entraConnector: { path: "/connectors/entra/:id", element: <Pages.DetailsEntra />, },
            profile: {
                path: "/profile", element: <Pages.Profile />,
            }
        }
    }
};
