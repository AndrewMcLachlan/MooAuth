import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createApipicklistDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateApiPickListDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createApipicklistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
