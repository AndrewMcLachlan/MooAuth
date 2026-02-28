import { IconButton } from "@andrewmclachlan/moo-ds";
import { ConnectorGroup } from "api";
import { useNavigate } from "react-router-dom";

interface GroupRowProps {
    group: ConnectorGroup;
}

export const GroupRow: React.FC<GroupRowProps> = ({ group }) => {
    const navigate = useNavigate();

    const handleManageRoles = (e: React.MouseEvent) => {
        e.stopPropagation();
        navigate(`/groups/${encodeURIComponent(group.id)}/roles`);
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
