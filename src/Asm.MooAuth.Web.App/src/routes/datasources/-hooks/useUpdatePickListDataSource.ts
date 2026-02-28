import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePicklistDataSourceMutation, getPicklistDataSourceQueryKey, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdatePickListDataSource = (id: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updatePicklistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getPicklistDataSourceQueryKey({ path: { id } }) });
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
