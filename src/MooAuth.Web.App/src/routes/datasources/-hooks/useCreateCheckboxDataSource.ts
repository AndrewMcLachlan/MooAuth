import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createCheckboxDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateCheckboxDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createCheckboxDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
