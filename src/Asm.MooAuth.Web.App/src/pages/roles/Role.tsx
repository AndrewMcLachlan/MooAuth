import { useIdParams } from "utils/useIdParams";
import { RoleProvider } from "./RoleProvider";
import { Navigate, Outlet, useMatch } from "react-router-dom";
import { useRole } from "services";

export const Role = () => {

    const id = useIdParams();

    const role = useRole(id);

    const match = useMatch("/accounts/:id");

    if (match) {
        return <Navigate to={`/roles/${id}/details`} replace />
    }

    if (!role.data)
    {
        return null;
    }

    return (
        <RoleProvider role={role.data}>
            <Outlet />
        </RoleProvider>
    );
}
