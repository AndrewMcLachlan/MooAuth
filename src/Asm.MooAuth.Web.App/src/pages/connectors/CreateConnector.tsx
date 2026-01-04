import { Page } from "@andrewmclachlan/moo-app";
import { LinkBox } from "@andrewmclachlan/moo-ds";
import { Col, Row } from "react-bootstrap";
import { useGetConnectorTypes } from "./hooks/useGetConnectorTypes";

export const CreateConnector = () => {

    var { data } = useGetConnectorTypes();

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
