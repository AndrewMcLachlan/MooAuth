import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateEntraConnectorMutation, getEntraConnectorQueryKey } from "../../../api/@tanstack/react-query.gen";

export const useUpdateEntraConnector = () => {
    const queryClient = useQueryClient();
    return useMutation({
        ...updateEntraConnectorMutation(),
        onSuccess: (_data, variables) => {
            queryClient.invalidateQueries({ queryKey: getEntraConnectorQueryKey({ path: { id: variables.path!.id } }) });
        },
    });
};
