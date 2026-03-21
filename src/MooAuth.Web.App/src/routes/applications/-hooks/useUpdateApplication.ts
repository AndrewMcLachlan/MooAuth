import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateApplicationMutation, getApplicationQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateApplication = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateApplicationMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getApplicationQueryKey({ path: { id: variables.path!.id } }) });
        },
    });
};
