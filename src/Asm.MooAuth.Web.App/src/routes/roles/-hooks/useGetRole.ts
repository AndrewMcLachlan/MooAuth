import { useQuery } from "@tanstack/react-query";
import { getRoleOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetRole = (id: number) => {
    return useQuery({
        ...getRoleOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
