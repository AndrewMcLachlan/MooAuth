import { DeleteIcon } from "@andrewmclachlan/mooapp";
import { Role } from "client";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteRole } from "services";

export const RoleRow: React.FC<RoleRowProps> = (props) => {

    const navigate = useNavigate();
    const deleteRole = useDeleteRole();

    const onRowClick = () => {
        navigate(`/roles/${props.role.id}/details`);
    };

    return (
        <tr onClick={onRowClick} className="clickable">
            <td>
                {props.role.logoUrl && <img src={props.role.logoUrl} alt={props.role.name} />}
            </td>
            <td>
                <div className="name">{props.role.name}</div>
            </td>
            <td>
                {props.role.description}
            </td>
            <td className="row-action">
                <DeleteIcon onClick={() => deleteRole(props.role.id!)} />
            </td>
        </tr>
    );
}

export interface RoleRowProps {
    role: Role;
}
