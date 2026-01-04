import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateStaticlistDataSourceMutation, getAllDataSourcesQueryKey, getStaticlistDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateStaticListDataSource = (id: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateStaticlistDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
            queryClient.invalidateQueries({ queryKey: getStaticlistDataSourceQueryKey({ path: { id } }) });
        },
    });
};
