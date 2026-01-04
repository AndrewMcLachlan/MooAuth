import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createRoleMutation, getAllRolesQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateRole = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createRoleMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllRolesQueryKey() });
        },
    });
};
