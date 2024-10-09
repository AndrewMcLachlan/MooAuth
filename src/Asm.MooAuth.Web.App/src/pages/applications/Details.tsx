import { DeleteIcon, EditColumn, Form, Page, SaveIcon, Section, SectionForm, SectionTable } from "@andrewmclachlan/mooapp";
import { CreateApplication, CreatePermission } from "client";
import React, { useEffect } from "react";
import { Button, Table } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useCreatePermission, useDeletePermission, useUpdateApplication, useUpdatePermission } from "services";
import { useApplication } from "./ApplicationProvider";
import { toNull } from "utils/toNull";
import { useIdParams } from "utils/useIdParams";

export const Details: React.FC = () => {

    const id = useIdParams();
    const application = useApplication();

    const update = useUpdateApplication();
    const createPermission = useCreatePermission();
    const updatePermission = useUpdatePermission();
    const deletePermission = useDeletePermission();

    const [newPermission, setNewPermission] = React.useState<CreatePermission>({ name: "", description: "" });


    const onSubmit: SubmitHandler<CreateApplication> = async (data: CreateApplication) => {
        update(application.id, data);
    };

    const { watch, formState, register, setValue, getValues, reset, handleSubmit, ...form } = useForm<CreateApplication>({
        defaultValues: {
            name: application.name,
            description: application.description,
            logoUrl: application.logoUrl,
        }
    });

    useEffect(() => {
        reset({
            name: application.name,
            description: application.description,
            logoUrl: application.logoUrl,
        });
    }, [id]);

    return (
        <Page title={application.name} breadcrumbs={[{ text: application.name, route: `/applications/${application.id}/details` }]}>
            <SectionForm onSubmit={handleSubmit(onSubmit)}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" defaultValue={application.name}{...register("name")} maxLength={50} />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" defaultValue={application.description ?? ""} {...register("description")} maxLength={255} />
                </Form.Group>
                <Form.Group groupId="logourl">
                    <Form.Label>Logo</Form.Label>
                    <Form.Input type="url" defaultValue={application.logoUrl ?? ""} {...register("logoUrl")} maxLength={255} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
            <SectionTable title="Permissions">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><input type="text" placeholder="Name" value={newPermission.name} onChange={e => setNewPermission({ ...newPermission, name: e.currentTarget.value })} className="form-control" /></td>
                        <td><input type="text" placeholder="Description" value={newPermission.description ?? ""} onChange={e => setNewPermission({ ...newPermission, description: toNull(e.currentTarget.value) })} className="form-control" /> </td>
                        <td className="row-action"><SaveIcon onClick={() => createPermission(application.id, newPermission)} /></td>
                    </tr>
                    {application?.permissions?.map((p) => (
                        <tr key={p.name}>
                            <EditColumn required value={p.name} maxLength={50} onChange={t => updatePermission(application.id, p.id, { ...p, name: t!.value })}>{p.name}</EditColumn>
                            <EditColumn value={p.description ?? ""} maxLength={255} onChange={t => updatePermission(application.id, p.id, { ...p, description: toNull(t?.value) })}>{p.description}</EditColumn>
                            <td className="row-action"><DeleteIcon onClick={() => deletePermission(application.id, p.id)} /></td>
                        </tr>
                    )
                    )}
                </tbody>
            </SectionTable>
        </Page >
    );
};
