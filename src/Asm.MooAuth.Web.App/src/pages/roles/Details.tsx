import { DeleteIcon, EditColumn, Form, Page, SaveIcon, Section, SectionForm, SectionTable } from "@andrewmclachlan/mooapp";
import { CreateRole, CreatePermission } from "client";
import React, { useEffect } from "react";
import { Button, Table } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import {  useUpdateRole, useAddPermission, useRemovePermission } from "services";
import { useRole } from "./RoleProvider";
import { toNull } from "utils/toNull";
import { useIdParams } from "utils/useIdParams";

export const Details: React.FC = () => {

    const id = useIdParams();
    const role = useRole();

    const update = useUpdateRole();
    const createPermission = useCreatePermission();
    const updatePermission = useUpdatePermission();
    const deletePermission = useDeletePermission();

    const [newPermission, setNewPermission] = React.useState<CreatePermission>({ name: "", description: "" });


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
            <SectionForm onSubmit={handleSubmit(onSubmit)}>
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
                        <td><input type="text" placeholder="Name" value={newPermission.name} onChange={e => setNewPermission({ ...newPermission, name: e.currentTarget.value })} className="form-control" /></td>
                        <td className="row-action"><SaveIcon onClick={() => createPermission(role.id!, newPermission)} /></td>
                    </tr>
                    {role?.permissions?.map((p) => (
                        <tr key={p.name}>
                            <td>{p.name}</td>
                            <td className="row-action"><DeleteIcon onClick={() => deletePermission(role.id!, p.id)} /></td>
                        </tr>
                    )
                    )}
                </tbody>
            </SectionTable>
        </Page >
    );
};
