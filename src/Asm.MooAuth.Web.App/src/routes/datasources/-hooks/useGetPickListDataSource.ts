import { useQuery } from "@tanstack/react-query";
import { getPicklistDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetPickListDataSource = (id: number) => {
    return useQuery({
        ...getPicklistDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
