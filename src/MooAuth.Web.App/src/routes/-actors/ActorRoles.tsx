import { Page } from "@andrewmclachlan/moo-app";
import { DeleteIcon, Form, IconButton, SectionForm } from "@andrewmclachlan/moo-ds";
import { ActorType, CreateActorRoleAssignment, DataSourceType, ResourceEntry } from "api";
import React, { useState } from "react";
import { Button, Col, Row, Table } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useGetActorRoleAssignments } from "./hooks/useGetActorRoleAssignments";
import { useAddRoleAssignment } from "./hooks/useAddRoleAssignment";
import { useRemoveRoleAssignment } from "./hooks/useRemoveRoleAssignment";
import { useGetRoles } from "../roles/-hooks/useGetRoles";
import { useGetDataSources } from "../datasources/-hooks/useGetDataSources";
import { useGetDataSourceValues } from "../datasources/-hooks/useGetDataSourceValues";

interface FormData {
    roleId: number;
    connectorId: number;
}

interface PendingResource {
    dataSourceId: number;
    dataSourceName: string;
    dataSourceType: DataSourceType;
    value: string;
    displayValue: string;
}

export interface ActorRolesProps {
    externalId: string;
    actorType: ActorType;
    title: string;
    parentRoute: string;
    parentText: string;
}

export const ActorRoles: React.FC<ActorRolesProps> = ({ externalId, actorType, title, parentRoute, parentText }) => {

    const { data: actor, isLoading } = useGetActorRoleAssignments(externalId, actorType);
    const { data: roles } = useGetRoles();
    const { data: dataSources } = useGetDataSources();

    const addRoleAssignment = useAddRoleAssignment(externalId, actorType);
    const removeRoleAssignment = useRemoveRoleAssignment(externalId, actorType);

    const [showAddForm, setShowAddForm] = useState(false);
    const [pendingResources, setPendingResources] = useState<PendingResource[]>([]);
    const [selectedDataSourceId, setSelectedDataSourceId] = useState<number | undefined>();
    const [freeTextValue, setFreeTextValue] = useState("");
    const [selectedListValue, setSelectedListValue] = useState("");

    const { data: dataSourceValues } = useGetDataSourceValues(selectedDataSourceId ?? 0);

    const selectedDataSource = dataSources?.find(ds => ds.id === selectedDataSourceId);

    const form = useForm<FormData>({
        defaultValues: {
            roleId: 0,
            connectorId: 1,
        }
    });

    const handleAddResource = () => {
        if (!selectedDataSource) return;

        if (selectedDataSource.type === "FreeText") {
            if (!freeTextValue.trim()) return;
            setPendingResources([...pendingResources, {
                dataSourceId: selectedDataSource.id,
                dataSourceName: selectedDataSource.name,
                dataSourceType: selectedDataSource.type,
                value: freeTextValue.trim(),
                displayValue: freeTextValue.trim(),
            }]);
            setFreeTextValue("");
        } else {
            if (!selectedListValue) return;
            const dsValue = dataSourceValues?.find(v => v.key === selectedListValue);
            setPendingResources([...pendingResources, {
                dataSourceId: selectedDataSource.id,
                dataSourceName: selectedDataSource.name,
                dataSourceType: selectedDataSource.type,
                value: selectedListValue,
                displayValue: dsValue?.displayValue ?? selectedListValue,
            }]);
            setSelectedListValue("");
        }
    };

    const handleRemovePending = (index: number) => {
        setPendingResources(pendingResources.filter((_, i) => i !== index));
    };

    const handleAddAssignment: SubmitHandler<FormData> = async (data) => {
        const resources: ResourceEntry[] = pendingResources.map(r => ({
            dataSourceId: r.dataSourceId,
            value: r.value,
        }));

        const assignment: CreateActorRoleAssignment = {
            roleId: data.roleId,
            connectorId: data.connectorId,
            resources,
        };

        addRoleAssignment.mutate(
            { path: { actorType, externalId }, body: assignment },
            {
                onSuccess: () => {
                    setShowAddForm(false);
                    form.reset();
                    setPendingResources([]);
                    setSelectedDataSourceId(undefined);
                    setFreeTextValue("");
                    setSelectedListValue("");
                }
            }
        );
    };

    const handleRemoveAssignment = (roleId: number) => {
        removeRoleAssignment.mutate({ path: { actorType, externalId, roleId } });
    };

    const handleCancel = () => {
        setShowAddForm(false);
        form.reset();
        setPendingResources([]);
        setSelectedDataSourceId(undefined);
        setFreeTextValue("");
        setSelectedListValue("");
    };

    const formatResources = (resources?: { dataSourceName?: string | null; resourceValue?: string | null }[]) => {
        if (!resources || resources.length === 0) return "All resources";
        if (resources.length === 1 && !resources[0].dataSourceName && !resources[0].resourceValue) return "All resources";
        return resources.map(r => `${r.dataSourceName}: ${r.resourceValue}`).join(", ");
    };

    return (
        <Page title={title} breadcrumbs={[
            { text: parentText, route: parentRoute },
            { text: "Manage Roles", route: `${parentRoute}/${externalId}/roles` }
        ]}
            actions={[<IconButton key="add-role" icon="plus" onClick={() => setShowAddForm(true)}>Add Role</IconButton>]}>
            <h4>Role Assignments</h4>
            {showAddForm && (
                <SectionForm form={form} onSubmit={handleAddAssignment} title="Add Role Assignment">
                    <Row>
                        <Col md={6}>
                            <Form.Group groupId="roleId">
                                <Form.Label>Role</Form.Label>
                                <Form.Select required>
                                    <option value="">Select a role...</option>
                                    {roles?.map(role => (
                                        <option key={role.id} value={role.id}>{role.name}</option>
                                    ))}
                                </Form.Select>
                            </Form.Group>
                        </Col>
                    </Row>

                    <Row className="mt-3">
                        <Col><h5>Resources</h5></Col>
                    </Row>
                    <Row>
                        <Col md={4}>
                            <Form.Group groupId="dataSourceId">
                                <Form.Label>Data Source</Form.Label>
                                <Form.Select
                                    value={selectedDataSourceId ?? ""}
                                    onChange={(e) => {
                                        setSelectedDataSourceId(e.target.value ? Number(e.target.value) : undefined);
                                        setFreeTextValue("");
                                        setSelectedListValue("");
                                    }}
                                >
                                    <option value="">Select a data source...</option>
                                    {dataSources?.map(ds => (
                                        <option key={ds.id} value={ds.id}>{ds.name} ({ds.type})</option>
                                    ))}
                                </Form.Select>
                            </Form.Group>
                        </Col>
                        {selectedDataSource && (
                            <>
                                <Col md={4}>
                                    <Form.Group groupId="resourceValue">
                                        <Form.Label>Value</Form.Label>
                                        {selectedDataSource.type === "FreeText" ? (
                                            <Form.Input
                                                type="text"
                                                value={freeTextValue}
                                                onChange={(e) => setFreeTextValue(e.target.value)}
                                                placeholder="Enter value or expression"
                                            />
                                        ) : (
                                            <Form.Select
                                                value={selectedListValue}
                                                onChange={(e) => setSelectedListValue(e.target.value)}
                                            >
                                                <option value="">Select a value...</option>
                                                {dataSourceValues?.map(v => (
                                                    <option key={v.id} value={v.key}>{v.displayValue}</option>
                                                ))}
                                            </Form.Select>
                                        )}
                                    </Form.Group>
                                </Col>
                                <Col md={2} className="d-flex align-items-end">
                                    <Button
                                        variant="outline-primary"
                                        onClick={handleAddResource}
                                        disabled={selectedDataSource.type === "FreeText" ? !freeTextValue.trim() : !selectedListValue}
                                    >
                                        Add
                                    </Button>
                                </Col>
                            </>
                        )}
                    </Row>

                    {pendingResources.length > 0 && (
                        <Row className="mt-3">
                            <Col>
                                <div className="d-flex flex-wrap gap-2">
                                    {pendingResources.map((resource, index) => (
                                        <span key={index} className="badge bg-secondary d-flex align-items-center gap-1" style={{ fontSize: "0.9rem" }}>
                                            {resource.dataSourceName}: {resource.displayValue}
                                            <button
                                                type="button"
                                                className="btn-close btn-close-white ms-1"
                                                style={{ fontSize: "0.6rem" }}
                                                onClick={() => handleRemovePending(index)}
                                                aria-label="Remove"
                                            />
                                        </span>
                                    ))}
                                </div>
                            </Col>
                        </Row>
                    )}

                    <Row className="mt-3">
                        <Col>
                            <Button type="submit" variant="primary" className="me-2">Save Assignment</Button>
                            <Button variant="secondary" onClick={handleCancel}>Cancel</Button>
                        </Col>
                    </Row>
                </SectionForm>
            )}

            <Table className="section" hover striped>
                <thead>
                    <tr>
                        <th>Role</th>
                        <th>Resources</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {isLoading ? (
                        <tr><td colSpan={3}>Loading...</td></tr>
                    ) : actor?.roleAssignments && actor.roleAssignments.length > 0 ? (
                        actor.roleAssignments.map(assignment => (
                            <tr key={assignment.roleId}>
                                <td>{assignment.roleName}</td>
                                <td>{formatResources(assignment.resources)}</td>
                                <td className="row-action">
                                    <DeleteIcon onClick={() => handleRemoveAssignment(assignment.roleId)} />
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr><td colSpan={3}>No role assignments</td></tr>
                    )}
                </tbody>
            </Table>
        </Page>
    );
};
