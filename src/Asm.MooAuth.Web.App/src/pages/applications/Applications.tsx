import { IconButton, LoadingTableRows, Page } from "@andrewmclachlan/mooapp";
import { Table } from "react-bootstrap";
import { useApplications } from "services/applications";
import { useNavigate } from "react-router-dom";
import { ApplicationRow } from "./ApplicationRow";

export const Applications = () => {

    const navigate = useNavigate();

    const applicationsQuery = useApplications();

    const { data } = applicationsQuery;

    const applicationRows: React.ReactNode[] = data?.map(a => <ApplicationRow key={a.id} application={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Applications" breadcrumbs={[{ text: "Applications", route: "/applications" }]} actions={[<IconButton key="add" onClick={() => navigate("/applications/create")} icon="plus">Create Application</IconButton>]}>
            <Table className="accounts section" hover striped>
                <thead>
                    <tr>
                        <th>Logo</th>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {applicationRows}
                </tbody>
            </Table>
        </Page>
    );

}
