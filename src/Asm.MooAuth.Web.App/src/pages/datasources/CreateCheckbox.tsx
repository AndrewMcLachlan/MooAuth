import { Page } from "@andrewmclachlan/moo-app";
import { SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateCheckboxDataSource } from "api";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { DataSourceBaseFields } from "./components/DataSourceBaseFields";
import { useCreateCheckboxDataSource } from "./hooks/useCreateCheckboxDataSource";

export const CreateCheckbox: React.FC = () => {

    const navigate = useNavigate();
    const createDataSource = useCreateCheckboxDataSource();

    const handleSubmit = async (data: CreateCheckboxDataSource) => {
        createDataSource.mutate({ body: data }, {
            onSuccess: (response) => navigate(`/datasources/checkbox/${response.id}`),
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
};
