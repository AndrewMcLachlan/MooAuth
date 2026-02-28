import { Page } from "@andrewmclachlan/moo-app";
import { LinkBox } from "@andrewmclachlan/moo-ds";
import { Col, Row } from "react-bootstrap";

export const CreateDataSource = () => {
    return (
        <Page title="Create Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create` }]}>
            <Row>
                <Col md={2}>
                    <LinkBox to="/datasources/create/freetext" image="Free Text" />
                </Col>
                <Col md={2}>
                    <LinkBox to="/datasources/create/checkbox" image="Checkbox" />
                </Col>
                <Col md={2}>
                    <LinkBox to="/datasources/create/picklist" image="Pick List" />
                </Col>
            </Row>
        </Page>
    );
}
