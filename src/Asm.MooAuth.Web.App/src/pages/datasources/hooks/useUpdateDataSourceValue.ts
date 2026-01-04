import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateDataSourceValueMutation, getDataSourceValuesQueryKey, getStaticlistDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateDataSourceValue = (dataSourceId: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateDataSourceValueMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getDataSourceValuesQueryKey({ path: { dataSourceId } }) });
            queryClient.invalidateQueries({ queryKey: getStaticlistDataSourceQueryKey({ path: { id: dataSourceId } }) });
        },
    });
};
