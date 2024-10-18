import { IconButton, LoadingTableRows, Page } from "@andrewmclachlan/mooapp";
import { Table } from "react-bootstrap";
import { useConnectors } from "services";
import { useNavigate } from "react-router-dom";
import { ConnectorRow } from "./ConnectorRow";

export const Connectors = () => {

    const navigate = useNavigate();

    const connectorsQuery = useConnectors();

    const { data } = connectorsQuery;

    const connectorRows: React.ReactNode[] = data?.map(a => <ConnectorRow key={a.id} connector={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Connectors" breadcrumbs={[{ text: "Connectors", route: "/connectors" }]} actions={[<IconButton key="add" onClick={() => navigate("/connectors/create")} icon="plus">Create Connector</IconButton>]}>
            <Table className="accounts section" hover striped>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {connectorRows}
                </tbody>
            </Table>
        </Page>
    );

}
