import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createPicklistDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreatePickListDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createPicklistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
