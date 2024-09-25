import { useIdParams as useBaseIdParams } from '@andrewmclachlan/mooapp';

export const useIdParams = () : number => {
    const id = useBaseIdParams();

    return Number(id);
};
