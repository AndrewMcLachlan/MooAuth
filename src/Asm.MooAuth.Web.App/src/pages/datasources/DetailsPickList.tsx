import { Page } from "@andrewmclachlan/moo-app";
import { DeleteIcon, Form, IconButton, SectionForm } from "@andrewmclachlan/moo-ds";
import { CreateDataSourceValue, CreatePickListDataSource } from "api";
import React, { useEffect, useState } from "react";
import { Button, Col, Row, Table } from "react-bootstrap";
import { SubmitHandler, useForm } from "react-hook-form";
import { useIdParams } from "utils/useIdParams";
import { useAddDataSourceValue } from "./hooks/useAddDataSourceValue";
import { useDeleteDataSourceValue } from "./hooks/useDeleteDataSourceValue";
import { useGetPickListDataSource } from "./hooks/useGetPickListDataSource";
import { useUpdatePickListDataSource } from "./hooks/useUpdatePickListDataSource";

export const DetailsPickList: React.FC = () => {

    const id = useIdParams();
    const { data: dataSource } = useGetPickListDataSource(id);

    const updateDataSource = useUpdatePickListDataSource(id);
    const addValue = useAddDataSourceValue(id);
    const deleteValue = useDeleteDataSourceValue(id);

    const [showAddValue, setShowAddValue] = useState(false);

    const onSubmit: SubmitHandler<CreatePickListDataSource> = async (data: CreatePickListDataSource) => {
        updateDataSource.mutate({ path: { id }, body: data });
    };

    const form = useForm<CreatePickListDataSource>({
        defaultValues: {
            name: dataSource?.name,
            key: dataSource?.key,
            description: dataSource?.description,
            allowMultiple: dataSource?.allowMultiple,
        }
    });

    const valueForm = useForm<CreateDataSourceValue>({
        defaultValues: {
            key: "",
            displayValue: "",
            sortOrder: 0,
        }
    });

    useEffect(() => {
        form.reset({
            name: dataSource?.name,
            key: dataSource?.key,
            description: dataSource?.description,
            allowMultiple: dataSource?.allowMultiple,
        });
    }, [id, dataSource, form]);

    const handleAddValue = async (data: CreateDataSourceValue) => {
        addValue.mutate({ path: { dataSourceId: id }, body: data }, {
            onSuccess: () => {
                setShowAddValue(false);
                valueForm.reset();
            }
        });
    };

    const handleDeleteValue = (valueId: number) => {
        deleteValue.mutate({ path: { dataSourceId: id, valueId } });
    };

    if (!dataSource) {
        return null;
    }

    return (
        <Page title={dataSource.name} breadcrumbs={[{ text: dataSource.name, route: `/datasources/picklist/${dataSource.id}` }]}>
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
                <Form.Group groupId="allowMultiple" className="form-check">
                    <Form.Check />
                    <Form.Label className="form-check-label">Allow Multiple Selection</Form.Label>
                </Form.Group>
                <Button type="submit" variant="primary">Save</Button>
            </SectionForm>

            <section>
                <Row className="section-header">
                    <Col><h4>Values</h4></Col>
                    <Col className="actions">
                        <IconButton icon="plus" onClick={() => setShowAddValue(true)}>Add Value</IconButton>
                    </Col>
                </Row>

                {showAddValue && (
                    <SectionForm form={valueForm} onSubmit={handleAddValue} title="Add Value">
                        <Row>
                            <Col md={4}>
                                <Form.Group groupId="key">
                                    <Form.Label>Key</Form.Label>
                                    <Form.Input type="text" maxLength={100} required />
                                </Form.Group>
                            </Col>
                            <Col md={4}>
                                <Form.Group groupId="displayValue">
                                    <Form.Label>Display Value</Form.Label>
                                    <Form.Input type="text" maxLength={255} required />
                                </Form.Group>
                            </Col>
                            <Col md={2}>
                                <Form.Group groupId="sortOrder">
                                    <Form.Label>Sort Order</Form.Label>
                                    <Form.Input type="number" />
                                </Form.Group>
                            </Col>
                            <Col md={2} className="d-flex align-items-end">
                                <Button type="submit" variant="primary" className="me-2">Add</Button>
                                <Button variant="secondary" onClick={() => setShowAddValue(false)}>Cancel</Button>
                            </Col>
                        </Row>
                    </SectionForm>
                )}

                <Table className="section" hover striped>
                    <thead>
                        <tr>
                            <th>Key</th>
                            <th>Display Value</th>
                            <th>Sort Order</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {dataSource.values?.map(value => (
                            <tr key={value.id}>
                                <td>{value.key}</td>
                                <td>{value.displayValue}</td>
                                <td>{value.sortOrder}</td>
                                <td className="row-action">
                                    <DeleteIcon onClick={() => handleDeleteValue(value.id)} />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </section>
        </Page>
    );
};
