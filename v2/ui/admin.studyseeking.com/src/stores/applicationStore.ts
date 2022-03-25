import { defineStore } from "pinia";
import {Application, GetApplicationsResponse} from "../types/request/application";
import axios from "axios";

const apiUrl = "https://localhost:7242";

interface State {
    applications: Application[]
}

export const useApplicationStore = defineStore("application", {
    state: (): State => ({
        applications: []
    }),
    actions: {
        async getApplications() {
            const response = await axios.get<GetApplicationsResponse>(`${apiUrl}/applications`);
            this.applications = response.data.applications;
        }
    }
});

