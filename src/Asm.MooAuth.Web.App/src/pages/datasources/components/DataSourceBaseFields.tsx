import { Form } from "@andrewmclachlan/moo-ds";

export const DataSourceBaseFields: React.FC = () => (
    <>
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
    </>
);
