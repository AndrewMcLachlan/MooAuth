import { DeleteIcon } from "@andrewmclachlan/moo-ds";
import { SimpleConnector } from "api";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteConnector } from "./hooks/useDeleteConnector";

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
                <DeleteIcon onClick={() => deleteConnector.mutate({ path: { id: props.connector.id } })} />
            </td>
        </tr>
    );
}

export interface ConnectorRowProps {
    connector: SimpleConnector;
}
