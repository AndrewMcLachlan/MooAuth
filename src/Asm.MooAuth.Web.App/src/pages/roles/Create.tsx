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

    const { register, setValue, getValues, reset, ...form } = useForm<CreateRole>({
        defaultValues: {
            name: "",
            description: "",
            logoUrl: "",
        }
    });

    return (
        <Page title="Create Role" breadcrumbs={[{ text: "Create", route: `/roles/create` }]}>
            <SectionForm onSubmit={form.handleSubmit(handleSubmit)}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page >
    );
};
