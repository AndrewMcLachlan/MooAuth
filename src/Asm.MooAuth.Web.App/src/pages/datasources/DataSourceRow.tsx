import { DeleteIcon } from "@andrewmclachlan/moo-ds";
import { SimpleDataSource } from "api";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteDataSource } from "./hooks/useDeleteDataSource";

export const DataSourceRow: React.FC<DataSourceRowProps> = (props) => {

    const navigate = useNavigate();
    const deleteDataSource = useDeleteDataSource();

    const getTypeRoute = () => {
        switch (props.dataSource.type) {
            case "FreeText": return "freetext";
            case "StaticList": return "staticlist";
            case "ApiList": return "apilist";
            default: return "";
        }
    };

    const onRowClick = () => {
        navigate(`/datasources/${getTypeRoute()}/${props.dataSource.id}`);
    };

    return (
        <tr onClick={onRowClick} className="clickable">
            <td>
                <div className="name">{props.dataSource.name}</div>
            </td>
            <td>
                {props.dataSource.key}
            </td>
            <td>
                {props.dataSource.type}
            </td>
            <td className="row-action">
                <DeleteIcon onClick={() => deleteDataSource.mutate({ path: { id: props.dataSource.id } })} />
            </td>
        </tr>
    );
}

export interface DataSourceRowProps {
    dataSource: SimpleDataSource;
}
