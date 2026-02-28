import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateCheckboxDataSourceMutation, getCheckboxDataSourceQueryKey, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateCheckboxDataSource = (id: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateCheckboxDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getCheckboxDataSourceQueryKey({ path: { id } }) });
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
