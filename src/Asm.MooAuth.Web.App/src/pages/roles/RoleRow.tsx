import { DeleteIcon } from "@andrewmclachlan/moo-ds";
import { Role } from "api";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteRole } from "./hooks/useDeleteRole";

export const RoleRow: React.FC<RoleRowProps> = (props) => {

    const navigate = useNavigate();
    const deleteRole = useDeleteRole();

    const onRowClick = () => {
        navigate(`/roles/${props.role.id}/details`);
    };

    return (
        <tr onClick={onRowClick} className="clickable">
            <td>
                <div className="name">{props.role.name}</div>
            </td>
            <td>
                {props.role.description}
            </td>
            <td className="row-action">
                <DeleteIcon onClick={(e) => {
                    e.stopPropagation();
                    deleteRole.mutate({ path: { id: props.role.id } })
                }} />
            </td>
        </tr>
    );
}

export interface RoleRowProps {
    role: Role;
}
