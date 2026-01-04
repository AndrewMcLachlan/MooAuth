import { ConnectorGroup } from "api";

interface GroupRowProps {
    group: ConnectorGroup;
}

export const GroupRow: React.FC<GroupRowProps> = ({ group }) => {
    return (
        <tr>
            <td>{group.name}</td>
            <td>{group.description}</td>
        </tr>
    );
};
