import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createFreetextDataSourceMutation, getAllDataSourcesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateFreeTextDataSource = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createFreetextDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
        },
    });
};
