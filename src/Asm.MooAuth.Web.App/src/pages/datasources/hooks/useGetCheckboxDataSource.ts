import { useQuery } from "@tanstack/react-query";
import { getCheckboxDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetCheckboxDataSource = (id: number) => {
    return useQuery({
        ...getCheckboxDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
