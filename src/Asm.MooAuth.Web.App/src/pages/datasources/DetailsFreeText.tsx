import { Page } from "@andrewmclachlan/moo-app";
import { Form, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateFreeTextDataSource } from "api";
import React, { useEffect } from "react";
import { Button } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useIdParams } from "utils/useIdParams";
import { useGetFreeTextDataSource } from "./hooks/useGetFreeTextDataSource";
import { useUpdateFreeTextDataSource } from "./hooks/useUpdateFreeTextDataSource";

export const DetailsFreeText: React.FC = () => {

    const id = useIdParams();
    const { data: dataSource } = useGetFreeTextDataSource(id);

    const updateDataSource = useUpdateFreeTextDataSource(id);

    const onSubmit: SubmitHandler<CreateFreeTextDataSource> = async (data: CreateFreeTextDataSource) => {
        updateDataSource.mutate({ path: { id }, body: data });
    };

    const form = useForm<CreateFreeTextDataSource>({
        defaultValues: {
            name: dataSource?.name,
            key: dataSource?.key,
            description: dataSource?.description,
        }
    });

    useEffect(() => {
        form.reset({
            name: dataSource?.name,
            key: dataSource?.key,
            description: dataSource?.description,
        });
    }, [id, dataSource, form]);

    if (!dataSource) {
        return null;
    }

    return (
        <Page title={dataSource.name} breadcrumbs={[{ text: dataSource.name, route: `/datasources/freetext/${dataSource.id}` }]}>
            <SectionForm form={form} onSubmit={onSubmit} title="Details">
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} required />
                </Form.Group>
                <Form.Group groupId="key">
                    <Form.Label>Key</Form.Label>
                    <Form.Input type="text" maxLength={50} required placeholder="Unique identifier for this data source" />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.TextArea maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
