import React, { useContext } from "react";
import { Application } from "api";

export interface ApplicationProviderProps {
    application: Application;
}

export const ApplicationContext = React.createContext<Application>({
    id: 0,
    name: "",
    description: "",
});

export const ApplicationProvider: React.FC<React.PropsWithChildren<ApplicationProviderProps>> = ({ application, children }) => {

    return (
        <ApplicationContext.Provider value={application}>
            {children}
        </ApplicationContext.Provider>
    );
};

export const useApplication = () => useContext(ApplicationContext);
