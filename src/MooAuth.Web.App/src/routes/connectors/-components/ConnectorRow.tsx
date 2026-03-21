import { DeleteIcon } from "@andrewmclachlan/moo-ds";
import { SimpleConnector } from "api";
import React from "react";
import { useNavigate } from "@tanstack/react-router";
import { useDeleteConnector } from "../-hooks/useDeleteConnector";

export const ConnectorRow: React.FC<ConnectorRowProps> = (props) => {

    const navigate = useNavigate();
    const deleteConnector = useDeleteConnector();

    const onRowClick = () => {
        navigate({ to: `/connectors/${props.connector.type.toLowerCase()}/$id`, params: { id: String(props.connector.id) } });
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
