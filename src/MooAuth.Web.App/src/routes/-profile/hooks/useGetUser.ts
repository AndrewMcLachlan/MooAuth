import { useQuery } from "@tanstack/react-query";
import { getUserOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetUser = () => {
    return useQuery({
        ...getUserOptions(),
    });
};
