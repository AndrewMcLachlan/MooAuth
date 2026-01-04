import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteApplicationMutation, getAllApplicationsQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useDeleteApplication = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...deleteApplicationMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllApplicationsQueryKey() });
        },
    });
};
