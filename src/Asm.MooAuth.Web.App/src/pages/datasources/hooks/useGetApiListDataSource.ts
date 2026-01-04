import { useQuery } from "@tanstack/react-query";
import { getApilistDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetApiListDataSource = (id: number) => {
    return useQuery({
        ...getApilistDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
