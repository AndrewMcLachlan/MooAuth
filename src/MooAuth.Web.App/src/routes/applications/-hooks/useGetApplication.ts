import { useQuery } from "@tanstack/react-query";
import { getApplicationOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetApplication = (id: number) => {
    return useQuery({
        ...getApplicationOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
