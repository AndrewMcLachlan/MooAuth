import { Form, Page, SectionForm } from "@andrewmclachlan/mooapp";
import { User } from "client";
import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useUser } from "services";

export const Profile: React.FC = () => {

    const {data: me} = useUser();

    const handleSubmit = async (data: User) => {
    };

    useEffect(() => {
        reset(me);
    }, [me]);

    const { register, setValue, getValues, reset, ...form } = useForm<User>({ defaultValues: me  });

    return (
        <Page title="Profile" breadcrumbs={[{text: "Profile", route: "/profile"}]}>
            <SectionForm onSubmit={form.handleSubmit(handleSubmit)}>
                <Form.Group groupId="name">
                    <Form.Label>Name</Form.Label>
                    <Form.Input type="text" value={`${me?.firstName ?? ""} ${me?.lastName ?? ""}`} readOnly />
                </Form.Group>
                <Form.Group groupId="email">
                    <Form.Label>Email</Form.Label>
                    <Form.Input type="text" defaultValue={me?.emailAddress} readOnly />
                </Form.Group>
                <Button hidden type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
