import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deletePermissionMutation, getApplicationQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useDeletePermission = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...deletePermissionMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getApplicationQueryKey({ path: { id: variables.path!.applicationId } }) });
        },
    });
};
