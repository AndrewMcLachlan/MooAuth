import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { IconButton, LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Table } from "react-bootstrap";
import { useNavigate } from "@tanstack/react-router";
import { DataSourceRow } from "./-components/DataSourceRow";
import { useGetDataSources } from "./-hooks/useGetDataSources";

export const Route = createFileRoute("/datasources/")({
    component: DataSources,
});

function DataSources() {

    const navigate = useNavigate();

    const { data } = useGetDataSources();

    const dataSourceRows: React.ReactNode[] = data?.map(d => <DataSourceRow key={d.id} dataSource={d} />) ?? [<LoadingTableRows key={1} rows={2} cols={3} />];

    return (
        <Page title="Data Sources" breadcrumbs={[{ text: "Data Sources", route: "/datasources" }]} actions={[<IconButton key="add" onClick={() => navigate({ to: "/datasources/create" })} icon="plus">Create Data Source</IconButton>]}>
            <Table className="section" hover striped>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Key</th>
                        <th>Type</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {dataSourceRows}
                </tbody>
            </Table>
        </Page>
    );
}
