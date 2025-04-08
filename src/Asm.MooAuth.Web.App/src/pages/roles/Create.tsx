import { Form, Page, SectionForm } from "@andrewmclachlan/mooapp";
import { CreateRole } from "client";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useCreateRole } from "services";

export const Create: React.FC = () => {

    const create = useCreateRole();

    const handleSubmit = async (data: CreateRole) => {
        create(data);
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
