import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { Form, Section, SectionForm, ThemeSelector } from "@andrewmclachlan/moo-ds";
import { User } from "api";
import { useEffect } from "react";
import { Button } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { useGetUser } from "./-profile/hooks/useGetUser";

export const Route = createFileRoute("/profile")({
    component: Profile,
});

function Profile() {

    const { data: me } = useGetUser();

    const handleSubmit = async (_data: User) => {
    };

    const form = useForm<User>({ defaultValues: me });

    useEffect(() => {
        form.reset(me);
    }, [me, form]);

    return (
        <Page title="Profile" breadcrumbs={[{ text: "Profile", route: "/profile" }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
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
            <Section title="Theme">
                <ThemeSelector />
            </Section>
        </Page>
    );
}
