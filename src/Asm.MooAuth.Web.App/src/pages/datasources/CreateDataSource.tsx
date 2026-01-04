import { Page } from "@andrewmclachlan/moo-app";
import { LinkBox } from "@andrewmclachlan/moo-ds";
import { Col, Row } from "react-bootstrap";
import { useGetDataSourceTypes } from "./hooks/useGetDataSourceTypes";

export const CreateDataSource = () => {

    var { data } = useGetDataSourceTypes();

    const getIcon = (type: string) => {
        switch (type) {
            case "FreeText": return undefined;
            case "StaticList": return undefined;
            case "ApiList": return undefined;
            default: return undefined;
        }
    };

    return (
        <Page title="Create Data Source" breadcrumbs={[{ text: "Create", route: `/datasources/create` }]}>
            <Row>
                {data?.map((dataSourceType) =>
                    <Col md={2} key={dataSourceType.id}>
                        <LinkBox to={`/datasources/create/${dataSourceType.name.toLowerCase()}`} image={getIcon(dataSourceType.name)}>
                            {dataSourceType.name}
                        </LinkBox>
                    </Col>
                )}
            </Row>
        </Page>
    );
}
