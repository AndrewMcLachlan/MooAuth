import { DeleteIcon, Form, Page, SaveIcon, SectionForm, SectionTable } from "@andrewmclachlan/mooapp";
import { CreateEntraConnector } from "client";
import { PermissionSelector } from "components/PermissionSelector";
import React, { useEffect } from "react";
import { Button } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useUpdateEntraConnector } from "services";
import { useIdParams } from "utils/useIdParams";
import { useEntraConnector } from "services";

const mask = "*****************************";

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
            clientSecret: mask,
        }
    });

    useEffect(() => {
        reset({
            name: connector?.name,
            clientId: connector?.clientId,
            clientSecret: mask,
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
                    <Form.Input type="text" defaultValue={connector.name}{...register("name")} maxLength={50} required />
                </Form.Group>
                <Form.Group groupId="clientId">
                    <Form.Label>Client ID</Form.Label>
                    <Form.Input type="text" defaultValue={connector.clientId ?? ""} {...register("clientId")} maxLength={100} required />
                </Form.Group>
                <Form.Group groupId="clientSecret">
                    <Form.Label>Client Secret</Form.Label>
                    <Form.Input type="password" defaultValue={mask} onFocus={(e) => e.currentTarget.value = e.currentTarget.value === mask ? "" : e.currentTarget.value} {...register("clientSecret")} maxLength={100} required />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
