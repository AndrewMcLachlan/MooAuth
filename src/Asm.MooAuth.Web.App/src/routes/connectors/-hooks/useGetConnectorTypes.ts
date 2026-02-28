import { useQuery } from "@tanstack/react-query";
import { getAvailableConnectorTypesOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetConnectorTypes = () => {
    return useQuery({
        ...getAvailableConnectorTypesOptions(),
    });
};
