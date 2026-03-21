import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeDataSourceValueMutation, getDataSourceValuesQueryKey, getPicklistDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useDeleteDataSourceValue = (dataSourceId: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...removeDataSourceValueMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getDataSourceValuesQueryKey({ path: { dataSourceId } }) });
            queryClient.invalidateQueries({ queryKey: getPicklistDataSourceQueryKey({ path: { id: dataSourceId } }) });
        },
    });
};
