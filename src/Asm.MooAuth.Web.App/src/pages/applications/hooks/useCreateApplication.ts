import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createApplicationMutation, getAllApplicationsQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateApplication = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createApplicationMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllApplicationsQueryKey() });
        },
    });
};
