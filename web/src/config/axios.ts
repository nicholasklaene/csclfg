import router from "@/router";
import axios, { AxiosRequestConfig } from "axios";
import { useAuthStore } from "@/stores/auth";

const beforeRequest = async (
  config: AxiosRequestConfig
): Promise<AxiosRequestConfig> => {
  const authStore = useAuthStore();

  authStore.authenticationCheck();
  if (authStore.state.isAuthenticated) {
    let token = localStorage.getItem("id_token");

    if (authStore.tokenIsExpired()) {
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

axios.interceptors.request.use(beforeRequest, onReject);
