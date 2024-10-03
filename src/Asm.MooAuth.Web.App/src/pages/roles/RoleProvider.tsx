import React, { useContext } from "react";
import { Role } from "client";

export interface RoleProviderProps {
    role: Role;
}

export const RoleContext = React.createContext<Role>({
    id: 0,
    name: "",
    description: "",
});

export const RoleProvider: React.FC<React.PropsWithChildren<RoleProviderProps>> = ({ role, children }) => {

    return (
        <RoleContext.Provider value={role}>
            {children}
        </RoleContext.Provider>
    );
};

export const useRole = () => useContext(RoleContext);
