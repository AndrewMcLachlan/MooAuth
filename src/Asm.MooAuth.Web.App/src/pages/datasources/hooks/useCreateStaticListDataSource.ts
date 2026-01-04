import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createStaticlistDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateStaticListDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createStaticlistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
