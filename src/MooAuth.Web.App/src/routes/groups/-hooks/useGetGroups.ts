import { useQuery } from "@tanstack/react-query";
import { getGroupsOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetGroups = (page: number = 1, pageSize: number = 20, search?: string) => {
    return useQuery({
        ...getGroupsOptions({
            query: { Page: page, PageSize: pageSize, Search: search }
        }),
    });
};
