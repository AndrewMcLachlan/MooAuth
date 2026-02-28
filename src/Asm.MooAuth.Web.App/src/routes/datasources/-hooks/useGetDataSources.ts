import { useQuery } from "@tanstack/react-query";
import { getAllDataSourcesOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetDataSources = () => {
    return useQuery({
        ...getAllDataSourcesOptions(),
    });
};
