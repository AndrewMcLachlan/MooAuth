import { Page } from "@andrewmclachlan/moo-app";
import { IconButton, LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { ConnectorRow } from "./ConnectorRow";
import { useGetConnectors } from "./hooks/useGetConnectors";

export const Connectors = () => {

    const navigate = useNavigate();

    const { data } = useGetConnectors();

    const connectorRows: React.ReactNode[] = data?.map(a => <ConnectorRow key={a.id} connector={a} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Connectors" breadcrumbs={[{ text: "Connectors", route: "/connectors" }]} actions={[<IconButton key="add" onClick={() => navigate("/connectors/create")} icon="plus">Create Connector</IconButton>]}>
            <Table className="accounts section" hover striped>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Type</th>
                        <th>Client ID</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {connectorRows}
                </tbody>
            </Table>
        </Page>
    );

}
