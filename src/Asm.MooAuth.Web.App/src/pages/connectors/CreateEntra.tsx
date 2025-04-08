import { Form, Page, SectionForm } from "@andrewmclachlan/mooapp";
import { CreateEntraConnector } from "client";
import React from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useCreateEntraConnector } from "services";

export const CreateEntra: React.FC = () => {

    const create = useCreateEntraConnector();

    const handleSubmit = async (data: CreateEntraConnector) => {
        create(data);
    };

    const form = useForm<CreateEntraConnector>({
        defaultValues: {
            name: "",
            clientId: "",
            clientSecret: "",
            audience: "",
            config: {
                tenantId: "",
            }
        }
    });

    return (
        <Page title="Create Entra Connector" breadcrumbs={[{ text: "Create", route: `/connectors/create/entra` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} />
                </Form.Group>
                <Form.Group groupId="config.tenantId">
                    <Form.Label>Tenant ID</Form.Label>
                    <Form.Input type="text" maxLength={100} />
                </Form.Group>
                <Form.Group groupId="clientId">
                    <Form.Label>Client ID</Form.Label>
                    <Form.Input type="text" maxLength={100} />
                </Form.Group>
                <Form.Group groupId="clientSecret">
                    <Form.Label>Client Secret</Form.Label>
                    <Form.Password maxLength={100} />
                </Form.Group>
                <Form.Group groupId="audience">
                    <Form.Label>Audience</Form.Label>
                    <Form.Input type="text" maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page >
    );
};
