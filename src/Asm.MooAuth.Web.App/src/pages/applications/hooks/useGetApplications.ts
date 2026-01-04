import { useQuery } from "@tanstack/react-query";
import { getAllApplicationsOptions } from "../../../api/@tanstack/react-query.gen";

export const useGetApplications = () => {
    return useQuery({
        ...getAllApplicationsOptions(),
    });
};
