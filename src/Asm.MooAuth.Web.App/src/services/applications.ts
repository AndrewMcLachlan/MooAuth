import { useApiDelete, useApiGet, useApiPatch, useApiPost, useApiPutEmpty } from "@andrewmclachlan/mooapp";
import { Application, CreateApplication, CreatePermission } from "client";

const key: string = "applications";

export const useApplications = () => useApiGet<Application[]>([key], "/api/applications");

export const useApplication = (id: number) => useApiGet<Application>([key, id], `/api/applications/${id}`, {
    enabled: id !== undefined,
});

export const useCreateApplication = () => {

    const { mutate } = useApiPost<Application, null, CreateApplication>(() => "/api/applications");

    const create = (application: CreateApplication) => mutate([null, application]);

    return create;
}

export const useUpdateApplication = () => {

    const { mutate } = useApiPatch<Application, number, CreateApplication>((id) => `/api/applications/${id}`);

    const update = (id: number, application: CreateApplication) => mutate([id, application]);

    return update;
}

export const useDeleteApplication = () => {

    const { mutate } = useApiDelete<number>((id) => `/api/applications/${id}`);

    const deleteApplication = (id: number) => mutate(id);

    return deleteApplication;
}

export const useCreatePermission = () => {

    const { mutate } = useApiPost<Application, number, CreatePermission>((id) => `/api/applications/${id}/permissions`);

    const create = (id: number, permssion: CreatePermission) => mutate([id, permssion]);

    return create;
}

export const useUpdatePermission = () => {

    const { mutate } = useApiPatch<Application, { id: number, permissionId: number }, CreatePermission>(({ id, permissionId }) => `/api/applications/${id}/permissions/${permissionId}`);

    const update = (id: number, permissionId: number, permission: CreatePermission) => mutate([{ id, permissionId }, permission]);

    return update;
}

export const useDeletePermission = () => {

    const { mutate } = useApiDelete<{ id: number, permissionId: number }>(({ id, permissionId }) => `/api/applications/${id}/permissions/${permissionId}`);

    const deletePermssion = (id: number, permissionId: number) => mutate({ id, permissionId });

    return deletePermssion;
}
