import React from "react";
import { useParams } from "react-router-dom";
import { ActorRoles } from "../actors";

export const GroupRoles: React.FC = () => {
    const { externalId } = useParams<{ externalId: string }>();

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
};
