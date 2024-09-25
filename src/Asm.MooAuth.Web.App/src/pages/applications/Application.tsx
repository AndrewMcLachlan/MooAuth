import { useIdParams } from "utils/useIdParams";
import { ApplicationProvider } from "./ApplicationProvider";
import { Navigate, Outlet, useMatch } from "react-router-dom";
import { useApplication } from "services";

export const Application = () => {

    const id = useIdParams();

    const application = useApplication(id);

    const match = useMatch("/accounts/:id");

    if (match) {
        return <Navigate to={`/applications/${id}/details`} replace />
    }

    if (!application.data)
    {
        return null;
    }

    return (
        <ApplicationProvider application={application.data}>
            <Outlet />
        </ApplicationProvider>
    );
}
