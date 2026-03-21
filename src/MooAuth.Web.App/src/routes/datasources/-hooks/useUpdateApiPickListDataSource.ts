import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateApipicklistDataSourceMutation, getApipicklistDataSourceQueryKey, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateApiPickListDataSource = (id: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateApipicklistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getApipicklistDataSourceQueryKey({ path: { id } }) });
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
