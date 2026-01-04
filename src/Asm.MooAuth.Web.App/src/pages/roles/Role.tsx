import { useIdParams } from "utils/useIdParams";
import { RoleProvider } from "./RoleProvider";
import { Navigate, Outlet, useMatch } from "react-router-dom";
import { useGetRole } from "./hooks/useGetRole";

export const Role = () => {

    const id = useIdParams();

    const role = useGetRole(id);

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
