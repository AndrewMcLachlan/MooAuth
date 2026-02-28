import { useQuery } from "@tanstack/react-query";
import { getActorWithRolesOptions } from "../../../api/@tanstack/react-query.gen";
import { ActorType } from "api";

export const useGetActorRoleAssignments = (externalId: string, actorType: ActorType) => {
    return useQuery({
        ...getActorWithRolesOptions({ path: { actorType, externalId } }),
        enabled: !!externalId,
    });
};
