import axios, { AxiosRequestConfig } from "axios";
import router from "../router";
import { tokenIsExpired } from "../utils/auth";
import { useAuthStore } from "../stores/authStore";

export function setHttpInterceptors(): void {
  axios.interceptors.request.use(beforeRequest, onReject);
}

const beforeRequest = async (
  config: AxiosRequestConfig
): Promise<AxiosRequestConfig> => {
  const authStore = useAuthStore();

  if (authStore._isAuthenticated) {
    let token = localStorage.getItem("id_token");

    if (tokenIsExpired()) {
      const success = await authStore.refresh();
      if (!success) {
        router.push({ name: "Home" });
      }
      token = localStorage.getItem("id_token");
    }

    if (config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    } else {
      router.push({ name: "Home" });
    }
  }

  return config;
};

const onReject = (error: any) => Promise.reject(error);
