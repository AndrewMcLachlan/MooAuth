import React from "react";
import { useParams } from "react-router-dom";
import { ActorRoles } from "../actors";

export const UserRoles: React.FC = () => {
    const { externalId } = useParams<{ externalId: string }>();

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
};
