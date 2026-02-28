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
            createCheckboxDataSource: { path: "/datasources/create/checkbox", element: <Pages.CreateCheckbox /> },
            createPickListDataSource: { path: "/datasources/create/picklist", element: <Pages.CreatePickList /> },
            freeTextDataSource: { path: "/datasources/freetext/:id", element: <Pages.DetailsFreeText />, },
            checkboxDataSource: { path: "/datasources/checkbox/:id", element: <Pages.DetailsCheckbox />, },
            pickListDataSource: { path: "/datasources/picklist/:id", element: <Pages.DetailsPickList />, },
            apiPickListDataSource: { path: "/datasources/apipicklist/:id", element: <Pages.DetailsApiPickList />, },
            users: { path: "/users", element: <Pages.Users /> },
            userRoles: { path: "/users/:externalId/roles", element: <Pages.UserRoles /> },
            groups: { path: "/groups", element: <Pages.Groups /> },
            groupRoles: { path: "/groups/:externalId/roles", element: <Pages.GroupRoles /> },
            profile: {
                path: "/profile", element: <Pages.Profile />,
            }
        }
    }
};
