import { createFileRoute } from "@tanstack/react-router";
import { Page } from "@andrewmclachlan/moo-app";

export const Route = createFileRoute("/$")({
    component: NotFound,
});

function NotFound() {
    return (
        <Page title="Not Found" breadcrumbs={[{ text: "Not Found", route: "" }]}>
            <p>The page you are looking for does not exist.</p>
        </Page>
    );
}
