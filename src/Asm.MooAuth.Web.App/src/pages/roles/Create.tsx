import { Page } from "@andrewmclachlan/moo-app";
import { Form, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateRole } from "api";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useCreateRole } from "./hooks/useCreateRole";

export const Create: React.FC = () => {

    const createRole = useCreateRole();

    const handleSubmit = async (data: CreateRole) => {
        createRole.mutate({ body: data });
    };

    const form = useForm<CreateRole>({
        defaultValues: {
            name: "",
            description: "",
        }
    });

    return (
        <Page title="Create Role" breadcrumbs={[{ text: "Create", route: `/roles/create` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page >
    );
};
