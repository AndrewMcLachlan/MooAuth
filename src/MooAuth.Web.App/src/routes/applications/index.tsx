import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Table } from "react-bootstrap";
import { ApplicationRow } from "./-components/ApplicationRow";
import { useGetApplications } from "./-hooks/useGetApplications";

export const Route = createFileRoute("/applications/")({
    component: Applications,
});

function Applications() {
    const { data } = useGetApplications();

    const applicationRows: React.ReactNode[] = data?.map(a => <ApplicationRow key={a.id} application={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Applications" breadcrumbs={[{ text: "Applications", route: "/applications" }]} actions={[{ id: "add", label: "Create Application", icon: "plus", to: "/applications/create" }]}>
            <Table className="accounts section" hover striped>
                <thead>
                    <tr>
                        <th>Logo</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th className="row-action column-5" />
                    </tr>
                </thead>
                <tbody>
                    {applicationRows}
                </tbody>
            </Table>
        </Page>
    );
}
