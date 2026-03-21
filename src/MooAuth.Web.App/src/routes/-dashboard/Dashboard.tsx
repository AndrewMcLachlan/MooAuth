import { Dashboard as DashboardPage, DashboardProps } from "@andrewmclachlan/moo-app";

const props: DashboardProps = {
    title: "Home",
}

export const Dashboard: React.FC = () =>
    <DashboardPage {...props}>
    </DashboardPage>
