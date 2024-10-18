import { LinkBox, Page } from "@andrewmclachlan/mooapp";
import { Col, Row } from "react-bootstrap";
import { useConnectorTypes } from "services"

export const CreateConnector = () => {

    var { data } = useConnectorTypes();

    return (
        <Page title="Create Connector" breadcrumbs={[{ text: "Create", route: `/connectors/create` }]}>
            <Row>
                {data?.map((connectorType) =>
                    <Col md={2} key={connectorType.id}>
                        <LinkBox to={`/connectors/create/${connectorType.name.toLowerCase()}`} image={connectorType.logoUrl}>
                            {connectorType.name}
                        </LinkBox>
                    </Col>
                )}
            </Row>
        </Page>
    );
}
