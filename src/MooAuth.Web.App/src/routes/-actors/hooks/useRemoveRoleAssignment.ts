import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeRoleAssignmentMutation, getActorWithRolesQueryKey } from "../../../api/@tanstack/react-query.gen";
import { ActorType } from "api";

export const useRemoveRoleAssignment = (externalId: string, actorType: ActorType) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...removeRoleAssignmentMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getActorWithRolesQueryKey({ path: { actorType, externalId } }) });
        },
    });
};
