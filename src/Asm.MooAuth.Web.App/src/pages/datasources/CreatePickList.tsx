import { Page } from "@andrewmclachlan/moo-app";
import { DeleteIcon, Form, SectionForm } from "@andrewmclachlan/moo-ds";
import { ApiAuthType, CreateDataSourceValue } from "api";
import React, { useState } from "react";
import { Button, Col, Form as BsForm, Row, Table } from "react-bootstrap";
import { useForm, useWatch } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { DataSourceBaseFields } from "./components/DataSourceBaseFields";
import { useCreateApiPickListDataSource } from "./hooks/useCreateApiPickListDataSource";
import { useCreatePickListDataSource } from "./hooks/useCreatePickListDataSource";

type ValueSource = "manual" | "api";

interface CreatePickListForm {
    name: string;
    key: string;
    description: string;
    allowMultiple: boolean;
    valueSource: ValueSource;
    // API config fields
    endpoint: string;
    authType: ApiAuthType;
    tokenEndpoint: string;
    clientId: string;
    clientSecret: string;
    scope: string;
    apiKeyHeader: string;
    apiKey: string;
    keyPath: string;
    displayValuePath: string;
    cacheMinutes: number;
}

export const CreatePickList: React.FC = () => {

    const navigate = useNavigate();
    const createManualPickList = useCreatePickListDataSource();
    const createApiPickList = useCreateApiPickListDataSource();

    const [values, setValues] = useState<CreateDataSourceValue[]>([]);
    const [newValue, setNewValue] = useState<CreateDataSourceValue>({ key: "", displayValue: "", sortOrder: 0 });

    const form = useForm<CreatePickListForm>({
        defaultValues: {
            name: "",
            key: "",
            description: "",
            allowMultiple: false,
            valueSource: "manual",
            endpoint: "",
            authType: "None",
            tokenEndpoint: "",
            clientId: "",
            clientSecret: "",
            scope: "",
            apiKeyHeader: "X-API-Key",
            apiKey: "",
            keyPath: "id",
            displayValuePath: "name",
            cacheMinutes: 5,
        }
    });

    const valueSource = useWatch({ control: form.control, name: "valueSource" });
    const authType = useWatch({ control: form.control, name: "authType" });

    const handleAddValue = () => {
        if (newValue.key && newValue.displayValue) {
            setValues([...values, newValue]);
            setNewValue({ key: "", displayValue: "", sortOrder: values.length });
        }
    };

    const handleRemoveValue = (index: number) => {
        setValues(values.filter((_, i) => i !== index));
    };

    const handleSubmit = async (data: CreatePickListForm) => {
        if (data.valueSource === "manual") {
            createManualPickList.mutate({
                body: {
                    name: data.name,
                    key: data.key,
                    description: data.description,
                    allowMultiple: data.allowMultiple,
                    values: values,
                }
            }, {
                onSuccess: (response) => navigate(`/datasources/picklist/${response.id}`),
            });
        } else {
            createApiPickList.mutate({
                body: {
                    name: data.name,
                    key: data.key,
                    description: data.description,
                    config: {
                        allowMultiple: data.allowMultiple,
                        endpoint: data.endpoint,
                        authType: data.authType,
                        tokenEndpoint: data.tokenEndpoint || undefined,
                        clientId: data.clientId || undefined,
                        scope: data.scope || undefined,
                        apiKeyHeader: data.apiKeyHeader || undefined,
                        keyPath: data.keyPath || undefined,
                        displayValuePath: data.displayValuePath || undefined,
                        cacheMinutes: data.cacheMinutes,
                    },
                    clientSecret: data.clientSecret || undefined,
                    apiKey: data.apiKey || undefined,
                }
            }, {
                onSuccess: (response) => navigate(`/datasources/apipicklist/${response.id}`),
            });
        }
    };

    return (
        <Page title="Create Pick List Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create/picklist` }]}>
            <SectionForm form={form} onSubmit={handleSubmit}>
                <DataSourceBaseFields />

                <Form.Group groupId="allowMultiple" className="form-check">
                    <Form.Check />
                    <Form.Label className="form-check-label">Allow Multiple Selection</Form.Label>
                </Form.Group>

                <BsForm.Group className="mb-3">
                    <BsForm.Label>Value Source</BsForm.Label>
                    <BsForm.Check
                        type="radio"
                        label="Manual Values"
                        value="manual"
                        {...form.register("valueSource")}
                    />
                    <BsForm.Check
                        type="radio"
                        label="API Endpoint"
                        value="api"
                        {...form.register("valueSource")}
                    />
                </BsForm.Group>

                {valueSource === "manual" && (
                    <>
                        <h5>Values</h5>
                        <Row className="mb-3">
                            <Col md={4}>
                                <BsForm.Control
                                    type="text"
                                    placeholder="Key"
                                    value={newValue.key}
                                    onChange={(e) => setNewValue({ ...newValue, key: e.target.value })}
                                />
                            </Col>
                            <Col md={4}>
                                <BsForm.Control
                                    type="text"
                                    placeholder="Display Value"
                                    value={newValue.displayValue}
                                    onChange={(e) => setNewValue({ ...newValue, displayValue: e.target.value })}
                                />
                            </Col>
                            <Col md={2}>
                                <BsForm.Control
                                    type="number"
                                    placeholder="Sort Order"
                                    value={newValue.sortOrder}
                                    onChange={(e) => setNewValue({ ...newValue, sortOrder: parseInt(e.target.value) || 0 })}
                                />
                            </Col>
                            <Col md={2}>
                                <Button variant="secondary" onClick={handleAddValue}>Add</Button>
                            </Col>
                        </Row>
                        {values.length > 0 && (
                            <Table hover striped>
                                <thead>
                                    <tr>
                                        <th>Key</th>
                                        <th>Display Value</th>
                                        <th>Sort Order</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {values.map((value, index) => (
                                        <tr key={index}>
                                            <td>{value.key}</td>
                                            <td>{value.displayValue}</td>
                                            <td>{value.sortOrder}</td>
                                            <td className="row-action">
                                                <DeleteIcon onClick={() => handleRemoveValue(index)} />
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </Table>
                        )}
                    </>
                )}

                {valueSource === "api" && (
                    <>
                        <h5>API Configuration</h5>
                        <Form.Group groupId="endpoint">
                            <Form.Label>Endpoint URL</Form.Label>
                            <Form.Input type="url" maxLength={500} required />
                        </Form.Group>
                        <Row>
                            <Col md={6}>
                                <Form.Group groupId="keyPath">
                                    <Form.Label>Key Path</Form.Label>
                                    <Form.Input type="text" maxLength={100} placeholder="JSON path (default: id)" />
                                </Form.Group>
                            </Col>
                            <Col md={6}>
                                <Form.Group groupId="displayValuePath">
                                    <Form.Label>Display Value Path</Form.Label>
                                    <Form.Input type="text" maxLength={100} placeholder="JSON path (default: name)" />
                                </Form.Group>
                            </Col>
                        </Row>
                        <Form.Group groupId="cacheMinutes">
                            <Form.Label>Cache Duration (minutes)</Form.Label>
                            <Form.Input type="number" min={0} max={1440} />
                        </Form.Group>

                        <h5>Authentication</h5>
                        <Form.Group groupId="authType">
                            <Form.Label>Auth Type</Form.Label>
                            <Form.Select>
                                <option value="None">None</option>
                                <option value="ApiKey">API Key</option>
                                <option value="OAuthClientCredentials">OAuth Client Credentials</option>
                            </Form.Select>
                        </Form.Group>

                        {authType === "ApiKey" && (
                            <>
                                <Form.Group groupId="apiKeyHeader">
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
                                <Form.Group groupId="tokenEndpoint">
                                    <Form.Label>Token Endpoint</Form.Label>
                                    <Form.Input type="url" maxLength={500} />
                                </Form.Group>
                                <Form.Group groupId="clientId">
                                    <Form.Label>Client ID</Form.Label>
                                    <Form.Input type="text" maxLength={100} />
                                </Form.Group>
                                <Form.Group groupId="clientSecret">
                                    <Form.Label>Client Secret</Form.Label>
                                    <Form.Password maxLength={500} />
                                </Form.Group>
                                <Form.Group groupId="scope">
                                    <Form.Label>Scope</Form.Label>
                                    <Form.Input type="text" maxLength={255} />
                                </Form.Group>
                            </>
                        )}
                    </>
                )}

                <Button type="submit" variant="primary">Create Pick List</Button>
            </SectionForm>
        </Page>
    );
};
