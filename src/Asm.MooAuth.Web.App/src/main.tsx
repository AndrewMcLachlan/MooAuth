import { MooApp } from '@andrewmclachlan/moo-app';
import { Spinner } from '@andrewmclachlan/moo-ds';
import { createRouter } from '@tanstack/react-router';
import { createRoot } from 'react-dom/client';
import { routeTree } from "./routeTree.gen.ts";
import { client } from "./api/client.gen";

import { library } from "@fortawesome/fontawesome-svg-core";
import { faArrowsRotate, faCheck, faCheckCircle, faTrashAlt, faChevronDown, faChevronUp, faTimesCircle, faArrowLeft, faChevronRight, faCircleChevronLeft, faLongArrowUp, faLongArrowDown, faUpload, faXmark, faFilterCircleXmark, faInfoCircle, faPenToSquare, faPlus } from "@fortawesome/free-solid-svg-icons";
library.add(faArrowsRotate, faCheck, faCheckCircle, faTrashAlt, faChevronDown, faChevronUp, faTimesCircle, faArrowLeft, faLongArrowUp, faLongArrowDown, faChevronRight, faCircleChevronLeft, faUpload, faXmark, faFilterCircleXmark, faInfoCircle, faPenToSquare, faPlus);

interface AppConfig {
    clientId: string;
}

const scopes = ["api://mooauth.mclachlan.family/api.read"];

async function fetchConfig(): Promise<AppConfig> {
    const response = await fetch("/api/config");
    if (!response.ok) {
        throw new Error("Failed to fetch application configuration");
    }
    return response.json();
}

async function initializeApp() {
    const config = await fetchConfig();

    const root = createRoot(document.getElementById("root")!);

    const versionMeta = Array.from(document.getElementsByTagName("meta")).find((value) => value.getAttribute("name") === "application-version")!;
    versionMeta.content = import.meta.env.VITE_REACT_APP_VERSION;

    const router = createRouter({
        routeTree,
        defaultPreload: "intent",
        defaultPreloadStaleTime: 0,
        scrollRestoration: true,
        defaultPendingComponent: Spinner,
    });

    root.render(
        <MooApp clientId={config.clientId} client={client.instance} scopes={scopes} name="MooAuth" version={import.meta.env.VITE_REACT_APP_VERSION} copyrightYear={2024} router={router} />
    );
}

initializeApp();
