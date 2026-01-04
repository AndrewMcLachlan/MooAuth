import { RouteDefinition } from "@andrewmclachlan/moo-app";
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
            datasources: { path: "/datasources", element: <Pages.DataSources />, },
            createDataSource: { path: "/datasources/create", element: <Pages.CreateDataSource /> },
            createFreeTextDataSource: { path: "/datasources/create/freetext", element: <Pages.CreateFreeText /> },
            createStaticListDataSource: { path: "/datasources/create/staticlist", element: <Pages.CreateStaticList /> },
            createApiListDataSource: { path: "/datasources/create/apilist", element: <Pages.CreateApiList /> },
            freeTextDataSource: { path: "/datasources/freetext/:id", element: <Pages.DetailsFreeText />, },
            staticListDataSource: { path: "/datasources/staticlist/:id", element: <Pages.DetailsStaticList />, },
            apiListDataSource: { path: "/datasources/apilist/:id", element: <Pages.DetailsApiList />, },
            users: { path: "/users", element: <Pages.Users /> },
            groups: { path: "/groups", element: <Pages.Groups /> },
            profile: {
                path: "/profile", element: <Pages.Profile />,
            }
        }
    }
};
