import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createEntraConnectorMutation, getAllConnectorsQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useCreateEntraConnector = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...createEntraConnectorMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllConnectorsQueryKey() });
        },
    });
};
