import { DeleteIcon } from "@andrewmclachlan/mooapp";
import { SimpleConnector } from "client";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteConnector } from "services";

export const ConnectorRow: React.FC<ConnectorRowProps> = (props) => {

    const navigate = useNavigate();
    const deleteConnector = useDeleteConnector();

    const onRowClick = () => {
        navigate(`/connectors/${props.connector.type.toLowerCase()}/${props.connector.id}`);
    };

    return (
        <tr onClick={onRowClick} className="clickable">
            <td>
                <div className="name">{props.connector.name}</div>
            </td>
            <td>
                {props.connector.type}
            </td>
            <td>
                {props.connector.clientId}
            </td>
            <td className="row-action">
                <DeleteIcon onClick={() => deleteConnector(props.connector.id)} />
            </td>
        </tr>
    );
}

export interface ConnectorRowProps {
    connector: SimpleConnector;
}
