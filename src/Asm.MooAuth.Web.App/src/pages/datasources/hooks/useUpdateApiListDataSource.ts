import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateApilistDataSourceMutation, getAllDataSourcesQueryKey, getApilistDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateApiListDataSource = (id: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateApilistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
            queryClient.invalidateQueries({ queryKey: getApilistDataSourceQueryKey({ path: { id } }) });
        },
    });
};
