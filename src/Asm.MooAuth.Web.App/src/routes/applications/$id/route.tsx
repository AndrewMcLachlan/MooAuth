import { createFileRoute, Outlet } from "@tanstack/react-router";
import { ApplicationProvider } from "../-components/ApplicationProvider";
import { useGetApplication } from "../-hooks/useGetApplication";

export const Route = createFileRoute("/applications/$id")({
    component: ApplicationLayout,
});

function ApplicationLayout() {
    const { id } = Route.useParams();
    const application = useGetApplication(Number(id));

    if (!application.data) {
        return null;
    }

    return (
        <ApplicationProvider application={application.data}>
            <Outlet />
        </ApplicationProvider>
    );
}
