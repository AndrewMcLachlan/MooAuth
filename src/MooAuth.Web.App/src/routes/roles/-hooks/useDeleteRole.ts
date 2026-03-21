import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteRoleMutation, getAllRolesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useDeleteRole = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...deleteRoleMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllRolesQueryKey() });
        },
    });
};
