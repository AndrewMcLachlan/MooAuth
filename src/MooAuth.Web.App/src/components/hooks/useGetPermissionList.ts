import { useQuery } from "@tanstack/react-query";
import { getPermissionListOptions } from "../../api/@tanstack/react-query.gen";

export const useGetPermissionList = () => {
    return useQuery({
        ...getPermissionListOptions(),
    });
};
