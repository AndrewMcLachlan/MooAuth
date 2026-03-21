import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addDataSourceValueMutation, getDataSourceValuesQueryKey, getPicklistDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useAddDataSourceValue = (dataSourceId: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...addDataSourceValueMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getDataSourceValuesQueryKey({ path: { dataSourceId } }) });
            queryClient.invalidateQueries({ queryKey: getPicklistDataSourceQueryKey({ path: { id: dataSourceId } }) });
        },
    });
};
