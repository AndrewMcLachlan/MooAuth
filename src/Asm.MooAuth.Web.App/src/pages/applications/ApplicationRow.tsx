import { DeleteIcon } from "@andrewmclachlan/moo-ds";
import { Application } from "api";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteApplication } from "./hooks/useDeleteApplication";

export const ApplicationRow: React.FC<ApplicationRowProps> = (props) => {

    const navigate = useNavigate();
    const deleteApplication = useDeleteApplication();

    const onRowClick = () => {
        navigate(`/applications/${props.application.id}/details`);
    };

    return (
        <tr onClick={onRowClick} className="clickable">
            <td>
                {props.application.logoUrl && <img src={props.application.logoUrl} alt={props.application.name} />}
            </td>
            <td>
                <div className="name">{props.application.name}</div>
            </td>
            <td>
                {props.application.description}
            </td>
            <td className="row-action">
                <DeleteIcon onClick={(e) => {
                    e.stopPropagation();
                    deleteApplication.mutate({ path: { id: props.application.id } })
                }} />
            </td>
        </tr>
    );
}

export interface ApplicationRowProps {
    application: Application;
}
