import { createFileRoute, Outlet } from "@tanstack/react-router";
import { RoleProvider } from "../-components/RoleProvider";
import { useGetRole } from "../-hooks/useGetRole";

export const Route = createFileRoute("/roles/$id")({
    component: RoleLayout,
});

function RoleLayout() {
    const { id } = Route.useParams();
    const role = useGetRole(Number(id));

    if (!role.data) {
        return null;
    }

    return (
        <RoleProvider role={role.data}>
            <Outlet />
        </RoleProvider>
    );
}
