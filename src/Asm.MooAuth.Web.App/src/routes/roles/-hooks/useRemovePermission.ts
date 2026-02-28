import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeRoleMutation, getRoleQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useRemovePermission = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...removeRoleMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getRoleQueryKey({ path: { id: variables.path!.roleId } }) });
        },
    });
};
