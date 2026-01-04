import { ConnectorUser } from "api";

interface UserRowProps {
    user: ConnectorUser;
}

export const UserRow: React.FC<UserRowProps> = ({ user }) => {
    return (
        <tr>
            <td>{user.displayName}</td>
            <td>{user.email}</td>
            <td>{user.firstName}</td>
            <td>{user.lastName}</td>
        </tr>
    );
};
