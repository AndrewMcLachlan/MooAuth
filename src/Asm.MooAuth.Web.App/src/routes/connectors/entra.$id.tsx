import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { Form, passwordMask, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateEntraConnector } from "api";
import { useEffect } from "react";
import { Button } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useIdParams } from "utils/useIdParams";
import { useGetEntraConnector } from "./-hooks/useGetEntraConnector";
import { useUpdateEntraConnector } from "./-hooks/useUpdateEntraConnector";

export const Route = createFileRoute("/connectors/entra/$id")({
    component: DetailsEntra,
});

function DetailsEntra() {

    const id = useIdParams();
    const { data: connector } = useGetEntraConnector(id);

    const updateConnector = useUpdateEntraConnector();

    const onSubmit: SubmitHandler<CreateEntraConnector> = async (data: CreateEntraConnector) => {
        updateConnector.mutate({ path: { id }, body: data });
    };

    const form = useForm<CreateEntraConnector>({
        defaultValues: {
            name: connector?.name,
            config: { tenantId: connector?.config?.tenantId },
            clientId: connector?.clientId,
            clientSecret: passwordMask,
        }
    });

    useEffect(() => {
        form.reset({
            name: connector?.name,
            config: { tenantId: connector?.config?.tenantId },
            clientId: connector?.clientId,
            clientSecret: passwordMask,
        });
    }, [id, connector, form]);

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
}
