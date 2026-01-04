import { Page } from "@andrewmclachlan/moo-app";
import { LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Form, Table } from "react-bootstrap";
import { useState } from "react";
import { GroupRow } from "./GroupRow";
import { useGetGroups } from "./hooks/useGetGroups";

export const Groups = () => {
    const [search, setSearch] = useState<string>("");
    const [searchTerm, setSearchTerm] = useState<string>("");

    const { data, isLoading } = useGetGroups(1, 50, searchTerm || undefined);

    const handleSearchSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        setSearchTerm(search);
    };

    const groupRows: React.ReactNode[] = data?.results?.map(g =>
        <GroupRow key={g.id} group={g} />
    ) ?? (isLoading ? [<LoadingTableRows key={1} rows={5} cols={2} />] : []);

    return (
        <Page title="Groups" breadcrumbs={[{ text: "Groups", route: "/groups" }]}>
            <Form onSubmit={handleSearchSubmit} className="mb-3">
                <Form.Control
                    type="text"
                    placeholder="Search groups..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
            </Form>
            <Table className="section" hover striped>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {groupRows}
                </tbody>
            </Table>
            {data && <div className="text-muted">Showing {data.results.length} of {data.total} groups</div>}
        </Page>
    );
};
