import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateCheckboxDataSource } from "api";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "@tanstack/react-router";
import { DataSourceBaseFields } from "../-components/DataSourceBaseFields";
import { useCreateCheckboxDataSource } from "../-hooks/useCreateCheckboxDataSource";

export const Route = createFileRoute("/datasources/create/checkbox")({
    component: CreateCheckbox,
});

function CreateCheckbox() {

    const navigate = useNavigate();
    const createDataSource = useCreateCheckboxDataSource();

    const handleSubmit = async (data: CreateCheckboxDataSource) => {
        createDataSource.mutate({ body: data }, {
            onSuccess: (response) => navigate({ to: "/datasources/checkbox/$id", params: { id: String(response.id) } }),
        });
    };

    const form = useForm<CreateCheckboxDataSource>({
        defaultValues: {
            name: "",
            key: "",
            description: "",
        }
    });

    return (
        <Page title="Create Checkbox Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create/checkbox` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <DataSourceBaseFields />
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
}
