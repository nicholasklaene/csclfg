import { defineStore } from "pinia";
import axios from "axios";
import jwtDecode from "jwt-decode";

import { IdToken, AuthStoreState, AuthStoreInitialState } from "../types/auth";
import { tokenIsExpired, generatePKCE } from "../utils/auth";
import { clientId, authServerBaseURL, callbackUrl } from "../config";

export const useAuthStore = defineStore("auth", {
  state: (): AuthStoreState => AuthStoreInitialState,
  getters: {
    isAuthenticated: (state): boolean => {
      const idToken = localStorage.getItem("id_token");

      if (!idToken || tokenIsExpired()) return false;

      // Make sure state is updated on every route change
      const decodedIdToken: IdToken = jwtDecode(idToken);
      state.username = decodedIdToken.email;
      state.email = decodedIdToken.email;
      state.emailVerified = decodedIdToken.email_verified;

      return true;
    },
  },
  actions: {
    async exchangeCodeForAccessToken(
      code: string,
      state: string
    ): Promise<void> {
      const formData = new URLSearchParams();

      formData.append("grant_type", "authorization_code");
      formData.append("redirect_uri", callbackUrl);
      formData.append("client_id", clientId);
      formData.append("code_verifier", state);
      formData.append("code", code);

      const config = {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
      };

      // Get axios instance without defaults (authorization header is not allowed for this endpoint)
      const uninterceptedAxiosInstance = axios.create();

      const response = await uninterceptedAxiosInstance.post(
        `${authServerBaseURL}/oauth2/token`,
        formData,
        config
      );

      localStorage.setItem("refresh_token", response.data.refresh_token);
      localStorage.setItem("id_token", response.data.id_token);
      this.updateState(response.data.id_token);
    },
    async redirectToAuthServer(isLogin: boolean): Promise<void> {
      const { codeChallenge, codeVerifier } = await generatePKCE();

      const urlBuilder = new URL(
        `${authServerBaseURL}/${isLogin ? "login" : "signup"}`
      );

      urlBuilder.searchParams.append("response_type", "code");
      urlBuilder.searchParams.append("scope", "openid");
      urlBuilder.searchParams.append("client_id", clientId);
      urlBuilder.searchParams.append("redirect_uri", callbackUrl);
      urlBuilder.searchParams.append("state", codeVerifier);
      urlBuilder.searchParams.append("code_challenge", codeChallenge);
      urlBuilder.searchParams.append("code_challenge_method", "S256");

      const authServerRedirectURL: string = urlBuilder.href;

      window.location.replace(authServerRedirectURL);
    },
    // Returns true for success
    async refresh(): Promise<boolean> {
      const refreshToken = localStorage.getItem("refresh_token");

      if (!refreshToken) {
        return false;
      }

      const formData = new URLSearchParams();

      formData.append("grant_type", "refresh_token");
      formData.append("client_id", clientId);
      formData.append("refresh_token", refreshToken);

      const config = {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
      };

      // Get axios instance without defaults (authorization header is not allowed for this endpoint)
      const uninterceptedAxiosInstance = axios.create();

      try {
        const response = await uninterceptedAxiosInstance.post(
          `${authServerBaseURL}/oauth2/token`,
          formData,
          config
        );

        localStorage.setItem("id_token", response.data.id_token);
        this.updateState(response.data.id_token);
        return true;
      } catch (e) {
        return false;
      }
    },
    updateState(idToken: string) {
      const decodedIdToken: IdToken = jwtDecode(idToken);
      this.username = decodedIdToken.email;
      this.email = decodedIdToken.email;
      this.emailVerified = decodedIdToken.email_verified;
    },
    logout(): void {
      localStorage.removeItem("id_token");
      localStorage.removeItem("refresh_token");
    },
  },
});
