import { createFileRoute } from "@tanstack/react-router";
import { ActorRoles } from "../../-actors";

export const Route = createFileRoute("/groups/$externalId/roles")({
    component: GroupRoles,
});

function GroupRoles() {
    const { externalId } = Route.useParams();

    if (!externalId) {
        return null;
    }

    return (
        <ActorRoles
            externalId={externalId}
            actorType="Group"
            title="Manage Group Roles"
            parentRoute="/groups"
            parentText="Groups"
        />
    );
}
