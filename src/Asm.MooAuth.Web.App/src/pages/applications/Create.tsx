import { Form, Page, SectionForm } from "@andrewmclachlan/mooapp";
import { CreateApplication } from "client";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useCreateApplication } from "services";

export const Create: React.FC = () => {

    const create = useCreateApplication();

    const handleSubmit = async (data: CreateApplication) => {
        create(data);
    };

    const { register, setValue, getValues, reset, ...form } = useForm<CreateApplication>({
        defaultValues: {
            name: "",
            description: "",
            logoUrl: "",
        }
    });

    return (
        <Page title="Create Application" breadcrumbs={[{ text: "Create", route: `/applications/create` }]}>
            <SectionForm onSubmit={form.handleSubmit(handleSubmit)}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" {...register("name")} maxLength={50} />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" {...register("description")} maxLength={255} />
                </Form.Group>
                <Form.Group groupId="logourl">
                    <Form.Label>Logo</Form.Label>
                    <Form.Input type="url" {...register("logoUrl")} maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page >
    );
};
