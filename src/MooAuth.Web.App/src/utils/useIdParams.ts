import { useIdParams as useBaseIdParams } from '@andrewmclachlan/moo-app';

export const useIdParams = () : number => {
    const id = useBaseIdParams();

    return Number(id);
};
