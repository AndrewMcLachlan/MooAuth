import { ComboBox } from '@andrewmclachlan/moo-ds';
import { Permission } from 'api';
import React, { useMemo } from 'react';
import { useGetPermissionList } from './hooks/useGetPermissionList';

interface PermissionWithApplication extends Permission {
    applicationName: string;
}

export const PermissionSelector: React.FC<PermissionSelectProps> = ({ onChange, selectedPermissions = [] }) => {

    const { data } = useGetPermissionList();

    const permissionsToShow = useMemo(() => {
        const permissions: PermissionWithApplication[] = [];
        data?.forEach(app => {
            app.permissions?.forEach(p => {
                if (!selectedPermissions.some(sp => sp.id === p.id)) {
                    permissions.push({ ...p, applicationName: app.name! });
                }
            });
        });
        return permissions;
    }, [data, selectedPermissions]);

    return (
        <ComboBox<PermissionWithApplication>
            items={permissionsToShow}
            labelField={p => `${p.applicationName} - ${p.name}`}
            valueField={p => p.id}
            onChange={items => onChange?.(items[0])}
            clearable
        />
    );
}

export interface PermissionSelectProps {
    onChange?: (value?: Permission) => void;
    selectedPermissions?: Permission[];
}
