import { Page } from "@andrewmclachlan/moo-app";
import { SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateFreeTextDataSource } from "api";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { DataSourceBaseFields } from "./components/DataSourceBaseFields";
import { useCreateFreeTextDataSource } from "./hooks/useCreateFreeTextDataSource";

export const CreateFreeText: React.FC = () => {

    const navigate = useNavigate();
    const createDataSource = useCreateFreeTextDataSource();

    const handleSubmit = async (data: CreateFreeTextDataSource) => {
        createDataSource.mutate({ body: data }, {
            onSuccess: () => navigate("/datasources"),
        });
    };

    const form = useForm<CreateFreeTextDataSource>({
        defaultValues: {
            name: "",
            key: "",
            description: "",
        }
    });

    return (
        <Page title="Create Free Text Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create/freetext` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <DataSourceBaseFields />
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
