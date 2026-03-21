import { IconButton } from "@andrewmclachlan/moo-ds";
import { ConnectorUser } from "api";
import { useNavigate } from "@tanstack/react-router";

interface UserRowProps {
    user: ConnectorUser;
}

export const UserRow: React.FC<UserRowProps> = ({ user }) => {
    const navigate = useNavigate();

    const handleManageRoles = (e: React.MouseEvent) => {
        e.stopPropagation();
        navigate({ to: "/users/$externalId/roles", params: { externalId: encodeURIComponent(user.id) } });
    };

    return (
        <tr>
            <td>{user.displayName}</td>
            <td>{user.email}</td>
            <td>{user.firstName}</td>
            <td>{user.lastName}</td>
            <td className="row-action">
                <IconButton icon="user-gear" onClick={handleManageRoles} title="Manage Roles" />
            </td>
        </tr>
    );
};
