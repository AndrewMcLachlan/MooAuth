import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createPermissionMutation, getApplicationQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreatePermission = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createPermissionMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getApplicationQueryKey({ path: { id: variables.path!.applicationId } }) });
        },
    });
};
