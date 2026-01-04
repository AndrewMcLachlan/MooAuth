import { useQuery } from "@tanstack/react-query";
import { getStaticlistDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetStaticListDataSource = (id: number) => {
    return useQuery({
        ...getStaticlistDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
