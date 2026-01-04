import { useQuery } from "@tanstack/react-query";
import { getAllRolesOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetRoles = () => {
    return useQuery({
        ...getAllRolesOptions(),
    });
};
