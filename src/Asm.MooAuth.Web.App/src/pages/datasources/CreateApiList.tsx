import { Page } from "@andrewmclachlan/moo-app";
import { Form, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateApiListDataSource } from "api";
import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import { useForm, useWatch } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useCreateApiListDataSource } from "./hooks/useCreateApiListDataSource";

export const CreateApiList: React.FC = () => {

    const navigate = useNavigate();
    const createDataSource = useCreateApiListDataSource();

    const handleSubmit = async (data: CreateApiListDataSource) => {
        createDataSource.mutate({ body: data }, {
            onSuccess: () => navigate("/datasources"),
        });
    };

    const form = useForm<CreateApiListDataSource>({
        defaultValues: {
            name: "",
            key: "",
            description: "",
            config: {
                endpoint: "",
                authType: "None",
                tokenEndpoint: "",
                clientId: "",
                scope: "",
                apiKeyHeader: "X-API-Key",
                keyPath: "id",
                displayValuePath: "name",
                cacheMinutes: 5,
            },
            clientSecret: "",
            apiKey: "",
        }
    });

    const authType = useWatch({ control: form.control, name: "config.authType" });

    return (
        <Page title="Create API List Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create/apilist` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
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

                <h5>API Configuration</h5>
                <Form.Group groupId="config.endpoint">
                    <Form.Label>Endpoint URL</Form.Label>
                    <Form.Input type="url" maxLength={500} required />
                </Form.Group>
                <Row>
                    <Col md={6}>
                        <Form.Group groupId="config.keyPath">
                            <Form.Label>Key Path</Form.Label>
                            <Form.Input type="text" maxLength={100} placeholder="JSON path (default: id)" />
                        </Form.Group>
                    </Col>
                    <Col md={6}>
                        <Form.Group groupId="config.displayValuePath">
                            <Form.Label>Display Value Path</Form.Label>
                            <Form.Input type="text" maxLength={100} placeholder="JSON path (default: name)" />
                        </Form.Group>
                    </Col>
                </Row>
                <Form.Group groupId="config.cacheMinutes">
                    <Form.Label>Cache Duration (minutes)</Form.Label>
                    <Form.Input type="number" min={0} max={1440} />
                </Form.Group>

                <h5>Authentication</h5>
                <Form.Group groupId="config.authType">
                    <Form.Label>Auth Type</Form.Label>
                    <Form.Select>
                        <option value="None">None</option>
                        <option value="ApiKey">API Key</option>
                        <option value="OAuthClientCredentials">OAuth Client Credentials</option>
                    </Form.Select>
                </Form.Group>

                {authType === "ApiKey" && (
                    <>
                        <Form.Group groupId="config.apiKeyHeader">
                            <Form.Label>API Key Header</Form.Label>
                            <Form.Input type="text" maxLength={50} placeholder="Default: X-API-Key" />
                        </Form.Group>
                        <Form.Group groupId="apiKey">
                            <Form.Label>API Key</Form.Label>
                            <Form.Password maxLength={500} />
                        </Form.Group>
                    </>
                )}

                {authType === "OAuthClientCredentials" && (
                    <>
                        <Form.Group groupId="config.tokenEndpoint">
                            <Form.Label>Token Endpoint</Form.Label>
                            <Form.Input type="url" maxLength={500} />
                        </Form.Group>
                        <Form.Group groupId="config.clientId">
                            <Form.Label>Client ID</Form.Label>
                            <Form.Input type="text" maxLength={100} />
                        </Form.Group>
                        <Form.Group groupId="clientSecret">
                            <Form.Label>Client Secret</Form.Label>
                            <Form.Password maxLength={500} />
                        </Form.Group>
                        <Form.Group groupId="config.scope">
                            <Form.Label>Scope</Form.Label>
                            <Form.Input type="text" maxLength={255} />
                        </Form.Group>
                    </>
                )}

                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>
        </Page>
    );
};
