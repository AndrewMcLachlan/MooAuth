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

    const { watch, formState, register, setValue, getValues, reset, handleSubmit, ...form } = useForm<CreateEntraConnector>({
        defaultValues: {
            name: connector?.name,
            clientId: connector?.clientId,
            clientSecret: passwordMask,
        }
    });

    useEffect(() => {
        reset({
            name: connector?.name,
            clientId: connector?.clientId,
            clientSecret: passwordMask,
        });
    }, [id]);

    if (!connector) {
        return null;
    }

    return (
        <Page title={connector.name} breadcrumbs={[{ text: connector.name, route: `/connectors/${connector.id}/details` }]}>
            <SectionForm onSubmit={handleSubmit(onSubmit)} title="Details">
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" {...register("name")} maxLength={50} required />
                </Form.Group>
                <Form.Group groupId="config.tenantId">
                    <Form.Label>Tenant ID</Form.Label>
                    <Form.Input type="text" {...register("config.tenantId")} maxLength={100} />
                </Form.Group>
                <Form.Group groupId="clientId">
                    <Form.Label>Client ID</Form.Label>
                    <Form.Input type="text" {...register("clientId")} maxLength={100} required />
                </Form.Group>
                <Form.Group groupId="clientSecret">
                    <Form.Label>Client Secret</Form.Label>
                    <Form.Password {...register("clientSecret")} maxLength={100} />
                </Form.Group>
                <Form.Group groupId="audience">
                    <Form.Label>Audience</Form.Label>
                    <Form.Input type="text" defaultValue={""} {...register("audience")} maxLength={100} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
