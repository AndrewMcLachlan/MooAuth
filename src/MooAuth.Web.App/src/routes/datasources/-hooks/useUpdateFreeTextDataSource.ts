import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateFreetextDataSourceMutation, getAllDataSourcesQueryKey, getFreetextDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateFreeTextDataSource = (id: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateFreetextDataSourceMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllDataSourcesQueryKey() });
            queryClient.invalidateQueries({ queryKey: getFreetextDataSourceQueryKey({ path: { id } }) });
        },
    });
};
