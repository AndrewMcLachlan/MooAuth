import { DeleteIcon, Form, Page, SaveIcon, SectionForm, SectionTable } from "@andrewmclachlan/mooapp";
import { CreatePermission, CreateRole } from "client";
import { PermissionSelector } from "components/PermissionSelector";
import React, { useEffect } from "react";
import { Button } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useAddPermission, usePermissionsList, useRemovePermission, useUpdateRole } from "services";
import { useIdParams } from "utils/useIdParams";
import { useRole } from "./RoleProvider";

export const Details: React.FC = () => {

    const id = useIdParams();
    const role = useRole();

    const update = useUpdateRole();
    const addPermission = useAddPermission();
    const removePermission = useRemovePermission();
    const permissions = usePermissionsList();

    const [newPermissionId, setNewPermissionId] = React.useState<number>();


    const onSubmit: SubmitHandler<CreateRole> = async (data: CreateRole) => {
        update(role.id!, data);
    };

    const { watch, formState, register, setValue, getValues, reset, handleSubmit, ...form } = useForm<CreateRole>({
        defaultValues: {
            name: role.name,
            description: role.description,
        }
    });

    useEffect(() => {
        reset({
            name: role.name,
            description: role.description,
        });
    }, [id]);

    return (
        <Page title={role.name} breadcrumbs={[{ text: role.name, route: `/roles/${role.id}/details` }]}>
            <SectionForm onSubmit={handleSubmit(onSubmit)} title="Details">
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" defaultValue={role.name}{...register("name")} />
                </Form.Group>
                <Form.Group groupId="description">
                    <Form.Label>Description</Form.Label>
                    <Form.Input type="text" defaultValue={role.description ?? ""} {...register("description")} />
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
            <SectionTable title="Permissions">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><PermissionSelector onChange={p => setNewPermissionId(p?.id)} selectedPermissions={role.permissions} /></td>
                        <td className="row-action"><SaveIcon onClick={() => addPermission(role.id!, newPermissionId)} /></td>
                    </tr>
                    {role?.permissions?.map((p) => (
                        <tr key={p.name}>
                            <td>{p.name}</td>
                            <td className="row-action"><DeleteIcon onClick={() => removePermission(role.id!, p.id)} /></td>
                        </tr>
                    )
                    )}
                </tbody>
            </SectionTable>
        </Page >
    );
};
