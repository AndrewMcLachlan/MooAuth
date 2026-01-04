import { Page } from "@andrewmclachlan/moo-app";
import { Form, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateApplication } from "api";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useCreateApplication } from "./hooks/useCreateApplication";

export const Create: React.FC = () => {

    const createApplication = useCreateApplication();

    const handleSubmit = async (data: CreateApplication) => {
        createApplication.mutate({ body: data });
    };

    const form = useForm<CreateApplication>({
        defaultValues: {
            name: "",
            description: "",
            logoUrl: "",
        }
    });

    return (
        <Page title="Create Application" breadcrumbs={[{ text: "Create", route: `/applications/create` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" maxLength={255} />
                </Form.Group>
                <Form.Group groupId="logoUrl">
                    <Form.Label>Logo</Form.Label>
                    <Form.Input type="url" maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page >
    );
};
