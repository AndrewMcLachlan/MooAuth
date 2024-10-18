import { useApiDelete, useApiGet, useApiPatch, useApiPost } from "@andrewmclachlan/mooapp";
import { useQueryClient } from "@tanstack/react-query";
import { EntraConnector, CreateEntraConnector, SimpleConnector, ConnectorType, ConnectorTypeEntry } from "client";

const key: string = "connectors";

export const useConnectors = () => useApiGet<SimpleConnector[]>([key], "/api/connectors");

export const useConnectorTypes = () => useApiGet<ConnectorTypeEntry[]>([key, "types"], "/api/connectors/available");

export const useEntraConnector = (id: number) => useApiGet<EntraConnector>([key, id], `/api/connectors/entra/${id}`, {
    enabled: id !== undefined,
});

export const useCreateEntraConnector = () => {

    const { mutate } = useApiPost<EntraConnector, null, CreateEntraConnector>(() => "/api/connectors/entra");

    const create = (connector: CreateEntraConnector) => mutate([null, connector]);

    return create;
}

export const useUpdateEntraConnector = () => {

    const { mutate } = useApiPatch<EntraConnector, number, CreateEntraConnector>((id) => `/api/connectors/entra/${id}`);

    const update = (id: number, connector: CreateEntraConnector) => mutate([id, connector]);

    return update;
}

// TODO - Auth0

export const useDeleteConnector = () => {

    const { mutate } = useApiDelete<number>((id) => `/api/connectors/${id}`);

    const deleteConnector = (id: number) => mutate(id);

    return deleteConnector;
}
