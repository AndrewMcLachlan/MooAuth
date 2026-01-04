import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addPermissionMutation, getRoleQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useAddPermission = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...addPermissionMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getRoleQueryKey({ path: { id: variables.path!.roleId } }) });
        },
    });
};
