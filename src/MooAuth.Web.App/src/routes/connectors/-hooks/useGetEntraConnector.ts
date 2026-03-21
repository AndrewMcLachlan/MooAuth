import { useQuery } from "@tanstack/react-query";
import { getEntraConnectorOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetEntraConnector = (id: number) => {
    return useQuery({
        ...getEntraConnectorOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
