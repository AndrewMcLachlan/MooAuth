import { Permission } from 'client';
import React, { useMemo } from 'react';
import Select, { GroupBase } from 'react-select';
import { usePermissionsList } from 'services';

export const PermissionSelector: React.FC<PermissionSelectProps> = ({ onChange, selectedPermissions = [] }) => {

    var { data, isLoading } = usePermissionsList();

    const permisisonsToShow = useMemo(() => {
        return data?.filter(a => {
            a.permissions = a.permissions?.filter(p => !selectedPermissions.some(sp => sp.id === p.id));
            return a.permissions?.length! > 0;
        });
    }, [data, selectedPermissions]);

    const options: GroupBase<Permission>[] = permisisonsToShow?.map((application) => ({
        label: application.name!,
        options: application.permissions ?? [],
    })) ?? [];

    return (
        <Select<Permission> options={options} classNamePrefix="react-select" getOptionLabel={p => p.name} getOptionValue={p => p.id!.toString()!} onChange={a => onChange?.(a as Permission)} className="react-select" />
    );
}

export interface PermissionSelectProps {
    onChange?: (value?: Permission) => void;
    selectedPermissions?: Permission[];
}
