import { useApiDelete, useApiGet, useApiPatch, useApiPost, useApiPutEmpty } from "@andrewmclachlan/mooapp";
import { Role, CreateRole, CreatePermission } from "client";

const key: string = "roles";

export const useRoles = () => useApiGet<Role[]>([key], "/api/roles");

export const useRole = (id: number) => useApiGet<Role>([key, id], `/api/roles/${id}`, {
    enabled: id !== undefined,
});

export const useCreateRole = () => {

    const { mutate } = useApiPost<Role, null, CreateRole>(() => "/api/roles");

    const create = (role: CreateRole) => mutate([null, role]);

    return create;
}

export const useUpdateRole = () => {

    const { mutate } = useApiPatch<Role, number, CreateRole>((id) => `/api/roles/${id}`);

    const update = (id: number, role: CreateRole) => mutate([id, role]);

    return update;
}

export const useDeleteRole = () => {

    const { mutate } = useApiDelete<number>((id) => `/api/roles/${id}`);

    const deleteRole = (id: number) => mutate(id);

    return deleteRole;
}

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
