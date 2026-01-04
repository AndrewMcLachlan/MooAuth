import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createApilistDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateApiListDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createApilistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
