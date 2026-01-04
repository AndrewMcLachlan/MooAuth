import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addDataSourceValueMutation, getDataSourceValuesQueryKey, getStaticlistDataSourceQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useAddDataSourceValue = (dataSourceId: number) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...addDataSourceValueMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getDataSourceValuesQueryKey({ path: { dataSourceId } }) });
            queryClient.invalidateQueries({ queryKey: getStaticlistDataSourceQueryKey({ path: { id: dataSourceId } }) });
        },
    });
};
