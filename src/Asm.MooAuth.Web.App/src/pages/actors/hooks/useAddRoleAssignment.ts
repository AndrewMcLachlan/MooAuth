import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addRoleAssignmentMutation, getActorWithRolesQueryKey } from "../../../api/@tanstack/react-query.gen";
import { ActorType } from "api";

export const useAddRoleAssignment = (externalId: string, actorType: ActorType) => {
    const queryClient = useQueryClient();
    return useMutation({
        ...addRoleAssignmentMutation(),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: getActorWithRolesQueryKey({ path: { actorType, externalId } }) });
        },
    });
};
