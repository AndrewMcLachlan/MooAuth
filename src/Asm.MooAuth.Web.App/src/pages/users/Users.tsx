import { Page } from "@andrewmclachlan/moo-app";
import { LoadingTableRows } from "@andrewmclachlan/moo-ds";
import { Form, Table } from "react-bootstrap";
import { useState } from "react";
import { UserRow } from "./UserRow";
import { useGetUsers } from "./hooks/useGetUsers";

export const Users = () => {
    const [search, setSearch] = useState<string>("");
    const [searchTerm, setSearchTerm] = useState<string>("");

    const { data, isLoading } = useGetUsers(1, 50, searchTerm || undefined);

    const handleSearchSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        setSearchTerm(search);
    };

    const userRows: React.ReactNode[] = data?.results?.map(u =>
        <UserRow key={u.id} user={u} />
    ) ?? (isLoading ? [<LoadingTableRows key={1} rows={5} cols={4} />] : []);

    return (
        <Page title="Users" breadcrumbs={[{ text: "Users", route: "/users" }]}>
            <Form onSubmit={handleSearchSubmit} className="mb-3">
                <Form.Control
                    type="text"
                    placeholder="Search users..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
            </Form>
            <Table className="section" hover striped>
                <thead>
                    <tr>
                        <th>Display Name</th>
                        <th>Email</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                    </tr>
                </thead>
                <tbody>
                    {userRows}
                </tbody>
            </Table>
            {data && <div className="text-muted">Showing {data.results.length} of {data.total} users</div>}
        </Page>
    );
};
