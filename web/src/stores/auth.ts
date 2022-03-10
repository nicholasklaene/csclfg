import jwtDecode from "jwt-decode";
import axios from "axios";
import { reactive } from "vue";
import { defineStore } from "pinia";
import { IdToken, PKCE } from "../types/auth";
import { getApp } from "@/utils";
import { useRouter } from "vue-router";

export const useAuthStore = defineStore("auth", () => {
  let callbackUrl: string;
  if (getApp().includes("localhost")) {
    callbackUrl = "http://localhost:8080/oauth/callback";
  } else {
    callbackUrl = `https://${getApp()}.studyseeking.com/oauth/callback`;
  }

  const authServerBaseUrl = "https://identity.studyseeking.com";
  const clientId = "2lmskmbukto86mhvvqs666bime";
  const router = useRouter();

  const state = reactive({
    email: "",
    emailVerified: false,
    username: "",
    groups: [] as string[],
    isAdmin: false,
    isAuthenticated: false,
  });

  function authenticationCheck(): boolean {
    const token = localStorage.getItem("id_token");
    if (!token) return false;

    const expiredToken = tokenIsExpired();
    if (expiredToken) return false;

    updateState(token);
    return true;
  }

  function decTohex(dec: number): string {
    return ("0" + dec.toString(16)).substr(-2);
  }

  function sha256(plain: string): Promise<ArrayBuffer> {
    const encoder = new TextEncoder();
    const data = encoder.encode(plain);
    return window.crypto.subtle.digest("SHA-256", data);
  }

  function base64urlencode(a: ArrayBuffer): string {
    return btoa(String.fromCharCode.apply(null, Array.from(new Uint8Array(a))))
      .replace(/\+/g, "-")
      .replace(/\//g, "_")
      .replace(/=+$/, "");
  }

  function generateCodeVerifier(): string {
    const array = new Uint32Array(56 / 2);
    window.crypto.getRandomValues(array);
    return Array.from(array, decTohex).join("");
  }

  async function generateCodeChallenge(v: string): Promise<string> {
    const hashed: ArrayBuffer = await sha256(v);
    const base64encoded: string = base64urlencode(hashed);
    return base64encoded;
  }

  function tokenIsExpired(): boolean {
    const accessToken = localStorage.getItem("id_token");

    if (!accessToken) return true;

    const decodedToken: IdToken = jwtDecode(accessToken);

    const expirationTime = decodedToken.exp;
    const currentDate = new Date();
    const currentTime = currentDate.getTime() / 1000;

    return expirationTime < currentTime;
  }

  async function generatePKCE(): Promise<PKCE> {
    const codeVerifier = generateCodeVerifier();
    const codeChallenge = await generateCodeChallenge(codeVerifier);

    return {
      codeChallenge,
      codeVerifier,
    };
  }

  async function exchangeCodeForAccessToken(code: string, state: string) {
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

    const uninterceptedAxiosInstance = axios.create();

    const response = await uninterceptedAxiosInstance.post(
      `${authServerBaseUrl}/oauth2/token`,
      formData,
      config
    );

    localStorage.setItem("refresh_token", response.data.refresh_token);
    localStorage.setItem("id_token", response.data.id_token);
    updateState(response.data.id_token);
  }

  async function redirectToAuthServer(isLogin: boolean) {
    const { codeChallenge, codeVerifier } = await generatePKCE();

    const urlBuilder = new URL(
      `${authServerBaseUrl}/${isLogin ? "login" : "signup"}`
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
  }

  async function refresh() {
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

    const uninterceptedAxiosInstance = axios.create();

    try {
      const response = await uninterceptedAxiosInstance.post(
        `${authServerBaseUrl}/oauth2/token`,
        formData,
        config
      );

      localStorage.setItem("id_token", response.data.id_token);
      updateState(response.data.id_token);
      return true;
    } catch (e) {
      state.isAuthenticated = false;
      return false;
    }
  }

  function updateState(idToken: string) {
    const decodedIdToken: IdToken = jwtDecode(idToken);
    state.email = decodedIdToken.email;
    state.emailVerified = decodedIdToken.email_verified;
    state.username = decodedIdToken["cognito:username"];
    state.groups = decodedIdToken["cognito:groups"];
    state.isAdmin = state.groups.includes("admin");
    state.isAuthenticated = true;
  }

  function logout() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("refresh_token");

    state.email = "";
    state.emailVerified = false;
    state.username = "";
    state.isAdmin = false;
    state.isAuthenticated = false;

    router.push({ name: "Home" });
  }

  return {
    state,
    authenticationCheck,
    redirectToAuthServer,
    exchangeCodeForAccessToken,
    tokenIsExpired,
    refresh,
    logout,
  };
});
