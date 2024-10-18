import { Page } from "@andrewmclachlan/mooapp";
import { Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useConnectorTypes } from "services"

export const CreateConnector = () => {

    var { data } = useConnectorTypes();

    return (
        <Page title="Create Connector" breadcrumbs={[{ text: "Create", route: `/connectors/create` }]}>
        {data?.map((connectorType) =>
            <Link to={`/connectors/create/${connectorType.name.toLowerCase()}`} key={connectorType.id}>
                <Card>
                    <Card.Body>
                        <Card.Title>{connectorType.name}</Card.Title>
                        {connectorType.logoUrl && <Card.Img src={connectorType.logoUrl} />}
                    </Card.Body>
                </Card>
            </Link>
        )}
        </Page>
    );
}
