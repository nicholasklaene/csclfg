import { Category } from "./category";

export interface Application {
    id: number;
    name: string;
    subdomain: string;
    categories: Category[]
}

export interface GetApplicationsResponse {
    applications: Application[]
}
