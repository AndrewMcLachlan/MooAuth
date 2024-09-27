import { IconButton, LoadingTableRows, Page } from "@andrewmclachlan/mooapp";
import { Table } from "react-bootstrap";
import { useRoles } from "services";
import { useNavigate } from "react-router-dom";
import { RoleRow } from "./RoleRow";

export const Roles = () => {

    const navigate = useNavigate();

    const rolesQuery = useRoles();

    const { data } = rolesQuery;

    const roleRows: React.ReactNode[] = data?.map(a => <RoleRow key={a.id} role={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Roles" breadcrumbs={[{ text: "Roles", route: "/roles" }]} actions={[<IconButton key="add" onClick={() => navigate("/roles/create")} icon="plus">Create Role</IconButton>]}>
            <Table className="accounts section" hover striped>
                <thead>
                    <tr>
                        <th>Logo</th>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {roleRows}
                </tbody>
            </Table>
        </Page>
    );

}
