import { useQuery } from "@tanstack/react-query";
import { getAllConnectorsOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetConnectors = () => {
    return useQuery({
        ...getAllConnectorsOptions(),
    });
};
