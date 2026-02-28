import { useQuery } from "@tanstack/react-query";
import { getUsersOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetUsers = (page: number = 1, pageSize: number = 20, search?: string) => {
    return useQuery({
        ...getUsersOptions({
            query: { Page: page, PageSize: pageSize, Search: search }
        }),
    });
};
