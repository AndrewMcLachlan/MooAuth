import { Page } from "@andrewmclachlan/moo-app";
import { Form, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateStaticListDataSource } from "api";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useCreateStaticListDataSource } from "./hooks/useCreateStaticListDataSource";

export const CreateStaticList: React.FC = () => {

    const navigate = useNavigate();
    const createDataSource = useCreateStaticListDataSource();

    const handleSubmit = async (data: CreateStaticListDataSource) => {
        createDataSource.mutate({ body: data }, {
            onSuccess: (response) => navigate(`/datasources/staticlist/${response.id}`),
        });
    };

    const form = useForm<CreateStaticListDataSource>({
        defaultValues: {
            name: "",
            key: "",
            description: "",
            values: [],
        }
    });

    return (
        <Page title="Create Static List Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create/staticlist` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} required />
                </Form.Group>
                <Form.Group groupId="key">
                    <Form.Label>Key</Form.Label>
                    <Form.Input type="text" maxLength={50} required placeholder="Unique identifier for this data source" />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.TextArea maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
