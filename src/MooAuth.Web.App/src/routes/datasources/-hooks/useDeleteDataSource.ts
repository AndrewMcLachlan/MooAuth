import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useDeleteDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...deleteDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
