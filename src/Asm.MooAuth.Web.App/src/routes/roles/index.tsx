import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { IconButton, LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Table } from "react-bootstrap";
import { useNavigate } from "@tanstack/react-router";
import { RoleRow } from "./-components/RoleRow";
import { useGetRoles } from "./-hooks/useGetRoles";

export const Route = createFileRoute("/roles/")({
    component: Roles,
});

function Roles() {

    const navigate = useNavigate();

    const { data } = useGetRoles();

    const roleRows: React.ReactNode[] = data?.map(a => <RoleRow key={a.id} role={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Roles" breadcrumbs={[{ text: "Roles", route: "/roles" }]} actions={[<IconButton key="add" onClick={() => navigate({ to: "/roles/create" })} icon="plus">Create Role</IconButton>]}>
            <Table className="accounts section" hover striped>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th className="row-action column-5" />
                    </tr>
                </thead>
                <tbody>
                    {roleRows}
                </tbody>
            </Table>
        </Page>
    );
}
