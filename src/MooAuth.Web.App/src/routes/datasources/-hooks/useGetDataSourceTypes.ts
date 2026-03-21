import { useQuery } from "@tanstack/react-query";
import { getDataSourceTypesOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetDataSourceTypes = () => {
    return useQuery({
        ...getDataSourceTypesOptions(),
    });
};
