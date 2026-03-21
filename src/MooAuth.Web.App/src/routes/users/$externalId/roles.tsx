import { createFileRoute } from "@tanstack/react-router";
import { ActorRoles } from "../../-actors";

export const Route = createFileRoute("/users/$externalId/roles")({
    component: UserRoles,
});

function UserRoles() {
    const { externalId } = Route.useParams();

    if (!externalId) {
        return null;
    }

    return (
        <ActorRoles
            externalId={externalId}
            actorType="User"
            title="Manage User Roles"
            parentRoute="/users"
            parentText="Users"
        />
    );
}
