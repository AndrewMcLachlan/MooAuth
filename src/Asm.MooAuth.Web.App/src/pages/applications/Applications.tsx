import { Page } from "@andrewmclachlan/moo-app";
import { IconButton, LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { ApplicationRow } from "./ApplicationRow";
import { useGetApplications } from "./hooks/useGetApplications";

export const Applications = () => {

    const navigate = useNavigate();

    const { data } = useGetApplications();

    const applicationRows: React.ReactNode[] = data?.map(a => <ApplicationRow key={a.id} application={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Applications" breadcrumbs={[{ text: "Applications", route: "/applications" }]} actions={[<IconButton key="add" onClick={() => navigate("/applications/create")} icon="plus">Create Application</IconButton>]}>
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
