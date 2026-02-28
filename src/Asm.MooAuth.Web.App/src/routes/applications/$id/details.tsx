import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { DeleteIcon, EditColumn, Form, SaveIcon, SectionForm, SectionTable } from "@andrewmclachlan/moo-ds";
import { CreateApplication, CreatePermission } from "api";
import React, { useEffect } from "react";
import { Button } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useApplication } from "../-components/ApplicationProvider";
import { toNull } from "utils/toNull";
import { useIdParams } from "utils/useIdParams";
import { useUpdateApplication } from "../-hooks/useUpdateApplication";
import { useCreatePermission } from "../-hooks/useCreatePermission";
import { useUpdatePermission } from "../-hooks/useUpdatePermission";
import { useDeletePermission } from "../-hooks/useDeletePermission";

export const Route = createFileRoute("/applications/$id/details")({
    component: ApplicationDetails,
});

function ApplicationDetails() {

    const id = useIdParams();
    const application = useApplication();

    const updateApplication = useUpdateApplication();
    const createPermission = useCreatePermission();
    const updatePermission = useUpdatePermission();
    const deletePermission = useDeletePermission();

    const [newPermission, setNewPermission] = React.useState<CreatePermission>({ name: "", description: "" });


    const onSubmit: SubmitHandler<CreateApplication> = async (data: CreateApplication) => {
        updateApplication.mutate({ path: { id: application.id }, body: data });
    };

    const form = useForm<CreateApplication>({
        defaultValues: {
            name: application.name,
            description: application.description,
            logoUrl: application.logoUrl,
        }
    });

    useEffect(() => {
        form.reset({
            name: application.name,
            description: application.description,
            logoUrl: application.logoUrl,
        });
    }, [id, form]);

    return (
        <Page title={application.name} breadcrumbs={[{ text: application.name, route: `/applications/${application.id}/details` }]}>
            <SectionForm form={form} onSubmit={onSubmit}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" maxLength={50} />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" maxLength={255} />
                </Form.Group>
                <Form.Group groupId="logoUrl">
                    <Form.Label>Logo</Form.Label>
                    <Form.Input type="url" maxLength={255} />
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
                        <td className="row-action"><SaveIcon onClick={() => createPermission.mutate({ path: { applicationId: application.id }, body: newPermission })} /></td>
                    </tr>
                    {application?.permissions?.map((p) => (
                        <tr key={p.name}>
                            <EditColumn required value={p.name} maxLength={50} onChange={t => updatePermission.mutate({ path: { applicationId: application.id, id: p.id }, body: { name: t!.value } })}>{p.name}</EditColumn>
                            <EditColumn value={""} maxLength={255} onChange={t => updatePermission.mutate({ path: { applicationId: application.id, id: p.id }, body: { name: p.name, description: toNull(t?.value) } })}>{""}</EditColumn>
                            <td className="row-action"><DeleteIcon onClick={() => deletePermission.mutate({ path: { applicationId: application.id, id: p.id } })} /></td>
                        </tr>
                    )
                    )}
                </tbody>
            </SectionTable>
        </Page >
    );
}
