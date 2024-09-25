import { createMooAppBrowserRouter, MooApp } from '@andrewmclachlan/mooapp';
import { createRoot } from 'react-dom/client';
import { RouterProvider } from "react-router-dom";
import { routes } from "./Routes";

import { library } from "@fortawesome/fontawesome-svg-core";
import { faArrowsRotate, faCheck, faCheckCircle, faTrashAlt, faChevronDown, faChevronUp, faTimesCircle, faArrowLeft, faChevronRight, faCircleChevronLeft, faLongArrowUp, faLongArrowDown, faUpload, faXmark, faFilterCircleXmark, faInfoCircle, faPenToSquare, faPlus } from "@fortawesome/free-solid-svg-icons";
library.add(faArrowsRotate, faCheck, faCheckCircle, faTrashAlt, faChevronDown, faChevronUp, faTimesCircle, faArrowLeft, faLongArrowUp, faLongArrowDown, faChevronRight, faCircleChevronLeft, faUpload, faXmark, faFilterCircleXmark, faInfoCircle, faPenToSquare, faPlus);

const root = createRoot(document.getElementById("root")!);

const versionMeta = Array.from(document.getElementsByTagName("meta")).find((value) => value.getAttribute("name") === "application-version")!;
versionMeta.content = import.meta.env.VITE_REACT_APP_VERSION;

const router = createMooAppBrowserRouter(routes);

root.render(
    <MooApp clientId="a5726221-6abd-490b-ae3e-73f4bbe6d731" scopes={["api://mooauth.mclachlan.family/api.read"]} name="MooAuth" version={import.meta.env.VITE_REACT_APP_VERSION}>
        <RouterProvider router={router} />
    </MooApp>
);
