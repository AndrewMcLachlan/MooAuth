import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePermissionMutation, getApplicationQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdatePermission = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updatePermissionMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getApplicationQueryKey({ path: { id: variables.path!.applicationId } }) });
        },
    });
};
