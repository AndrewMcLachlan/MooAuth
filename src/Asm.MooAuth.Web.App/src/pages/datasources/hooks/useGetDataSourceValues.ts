import { useQuery } from "@tanstack/react-query";
import { getDataSourceValuesOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetDataSourceValues = (dataSourceId: number) => {
    return useQuery({
        ...getDataSourceValuesOptions({ path: { dataSourceId } }),
        enabled: dataSourceId !== undefined,
    });
};
