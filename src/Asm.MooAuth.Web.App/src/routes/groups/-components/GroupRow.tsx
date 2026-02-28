import { IconButton } from "@andrewmclachlan/moo-ds";
import { ConnectorGroup } from "api";
import { useNavigate } from "@tanstack/react-router";

interface GroupRowProps {
    group: ConnectorGroup;
}

export const GroupRow: React.FC<GroupRowProps> = ({ group }) => {
    const navigate = useNavigate();

    const handleManageRoles = (e: React.MouseEvent) => {
        e.stopPropagation();
        navigate({ to: "/groups/$externalId/roles", params: { externalId: encodeURIComponent(group.id) } });
    };

    return (
        <tr>
            <td>{group.name}</td>
            <td>{group.description}</td>
            <td className="row-action">
                <IconButton icon="user-gear" onClick={handleManageRoles} title="Manage Roles" />
            </td>
        </tr>
    );
};
