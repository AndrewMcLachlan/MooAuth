import { useApiDelete, useApiPutEmpty } from "@andrewmclachlan/mooapp";
//import { Role } from "client";
type Role = any;

export const useAddPermission = () => {

    const { mutate } = useApiPutEmpty<Role, { id: number, permissionId: number }>(({ id, permissionId }) => `/api/roles/${id}/permissions/${permissionId}`);

    const addPermission = (id: number, permissionId: number) => mutate({ id, permissionId });

    return addPermission;
}

export const useRemovePermission = () => {

    const { mutate } = useApiDelete<{ id: number, permissionId: number }>(({ id, permissionId }) => `/api/roles/${id}/permissions/${permissionId}`);

    const removePermission = ( id: number, permissionId: number ) => mutate({ id, permissionId });

    return removePermission;
}
