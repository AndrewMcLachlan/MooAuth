import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteConnectorMutation, getAllConnectorsQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useDeleteConnector = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...deleteConnectorMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getAllConnectorsQueryKey() });
        },
    });
};
