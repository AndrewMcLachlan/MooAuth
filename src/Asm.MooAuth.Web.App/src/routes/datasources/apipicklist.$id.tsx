import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";
import { Form, passwordMask, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateApiPickListDataSource } from "api";
import { useEffect } from "react";
import { Button, Col, Row } from "react-bootstrap";
import { SubmitHandler, useForm, useWatch } from "react-hook-form";
import { useIdParams } from "utils/useIdParams";
import { useGetApiPickListDataSource } from "./-hooks/useGetApiPickListDataSource";
import { useUpdateApiPickListDataSource } from "./-hooks/useUpdateApiPickListDataSource";

export const Route = createFileRoute("/datasources/apipicklist/$id")({
    component: DetailsApiPickList,
});

function DetailsApiPickList() {

    const id = useIdParams();
    const { data: dataSource } = useGetApiPickListDataSource(id);

    const updateDataSource = useUpdateApiPickListDataSource(id);

    const onSubmit: SubmitHandler<CreateApiPickListDataSource> = async (data: CreateApiPickListDataSource) => {
        updateDataSource.mutate({ path: { id }, body: data });
    };

    const form = useForm<CreateApiPickListDataSource>({
        defaultValues: {
            name: dataSource?.name,
            key: dataSource?.key,
            description: dataSource?.description,
            config: {
                allowMultiple: dataSource?.config?.allowMultiple,
                endpoint: dataSource?.config?.endpoint,
                authType: dataSource?.config?.authType,
                tokenEndpoint: dataSource?.config?.tokenEndpoint,
                clientId: dataSource?.config?.clientId,
                scope: dataSource?.config?.scope,
                apiKeyHeader: dataSource?.config?.apiKeyHeader,
                keyPath: dataSource?.config?.keyPath,
                displayValuePath: dataSource?.config?.displayValuePath,
                cacheMinutes: dataSource?.config?.cacheMinutes,
            },
            clientSecret: passwordMask,
            apiKey: passwordMask,
        }
    });

    const authType = useWatch({ control: form.control, name: "config.authType" });

    useEffect(() => {
        form.reset({
            name: dataSource?.name,
            key: dataSource?.key,
            description: dataSource?.description,
            config: {
                allowMultiple: dataSource?.config?.allowMultiple,
                endpoint: dataSource?.config?.endpoint,
                authType: dataSource?.config?.authType,
                tokenEndpoint: dataSource?.config?.tokenEndpoint,
                clientId: dataSource?.config?.clientId,
                scope: dataSource?.config?.scope,
                apiKeyHeader: dataSource?.config?.apiKeyHeader,
                keyPath: dataSource?.config?.keyPath,
                displayValuePath: dataSource?.config?.displayValuePath,
                cacheMinutes: dataSource?.config?.cacheMinutes,
            },
            clientSecret: passwordMask,
            apiKey: passwordMask,
        });
    }, [id, dataSource, form]);

    if (!dataSource) {
        return null;
    }

    return (
        <Page title={dataSource.name} breadcrumbs={[{ text: dataSource.name, route: `/datasources/apipicklist/${dataSource.id}` }]}>
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
                <Form.Group groupId="config.allowMultiple" className="form-check">
                    <Form.Check />
                    <Form.Label className="form-check-label">Allow Multiple Selection</Form.Label>
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
}
