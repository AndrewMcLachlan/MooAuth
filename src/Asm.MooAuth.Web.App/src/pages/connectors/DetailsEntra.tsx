import { Form, Page, passwordMask, SectionForm } from "@andrewmclachlan/mooapp";
import { CreateEntraConnector } from "client";
import React, { useEffect } from "react";
import { Button } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useEntraConnector, useUpdateEntraConnector } from "services";
import { useIdParams } from "utils/useIdParams";


export const DetailsEntra: React.FC = () => {

    const id = useIdParams();
    const {data: connector } = useEntraConnector(id);

    const update = useUpdateEntraConnector();

    const onSubmit: SubmitHandler<CreateEntraConnector> = async (data: CreateEntraConnector) => {
        update(id, data);
    };

    const form = useForm<CreateEntraConnector>({
        defaultValues: {
            name: connector?.name,
            clientId: connector?.clientId,
            clientSecret: passwordMask,
        }
    });

    useEffect(() => {
        form.reset({
            name: connector?.name,
            clientId: connector?.clientId,
            clientSecret: passwordMask,
        });
    }, [id, form]);

    if (!connector) {
        return null;
    }

    return (
        <Page title={connector.name} breadcrumbs={[{ text: connector.name, route: `/connectors/${connector.id}/details` }]}>
            <SectionForm form={form} onSubmit={onSubmit} title="Details">
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} required />
                </Form.Group>
                <Form.Group groupId="config.tenantId">
                    <Form.Label>Tenant ID</Form.Label>
                    <Form.Input type="text" maxLength={100} />
                </Form.Group>
                <Form.Group groupId="clientId">
                    <Form.Label>Client ID</Form.Label>
                    <Form.Input type="text" maxLength={100} required />
                </Form.Group>
                <Form.Group groupId="clientSecret">
                    <Form.Label>Client Secret</Form.Label>
                    <Form.Password maxLength={100} />
                </Form.Group>
                <Form.Group groupId="audience">
                    <Form.Label>Audience</Form.Label>
                    <Form.Input type="text" maxLength={100} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
