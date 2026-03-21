import { useQuery } from "@tanstack/react-query";
import { getFreetextDataSourceOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetFreeTextDataSource = (id: number) => {
    return useQuery({
        ...getFreetextDataSourceOptions({ path: { id } }),
        enabled: id !== undefined,
    });
};
