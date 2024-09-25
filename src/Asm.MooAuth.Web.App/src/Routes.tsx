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
        }
    }
};
