import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateRoleMutation, getRoleQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateRole = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateRoleMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getRoleQueryKey({ path: { id: variables.path!.id } }) });
        },
    });
};
