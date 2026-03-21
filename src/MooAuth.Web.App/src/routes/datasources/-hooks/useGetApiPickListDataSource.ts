import { useQuery } from "@tanstack/react-query";
import { getApipicklistDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetApiPickListDataSource = (id: number) => {
    return useQuery({
        ...getApipicklistDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
