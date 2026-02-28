import { createFileRoute, redirect } from "@tanstack/react-router";

export const Route = createFileRoute("/roles/$id/")({
    beforeLoad: ({ params }) => {
        throw redirect({
            to: "/roles/$id/details",
            params: { id: params.id },
        });
    },
});
