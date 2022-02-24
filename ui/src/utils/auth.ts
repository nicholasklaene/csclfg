import jwtDecode from "jwt-decode";
import { IdToken, PKCE } from "../types/auth";

const decTohex = (dec: number): string => {
  return ("0" + dec.toString(16)).substr(-2);
};

const sha256 = (plain: string): Promise<ArrayBuffer> => {
  const encoder = new TextEncoder();
  const data = encoder.encode(plain);
  return window.crypto.subtle.digest("SHA-256", data);
};

const base64urlencode = (a: ArrayBuffer): string => {
  return btoa(String.fromCharCode.apply(null, Array.from(new Uint8Array(a))))
    .replace(/\+/g, "-")
    .replace(/\//g, "_")
    .replace(/=+$/, "");
};

const generateCodeVerifier = (): string => {
  const array = new Uint32Array(56 / 2);
  window.crypto.getRandomValues(array);
  return Array.from(array, decTohex).join("");
};

const generateCodeChallenge = async (v: string): Promise<string> => {
  const hashed: ArrayBuffer = await sha256(v);
  const base64encoded: string = base64urlencode(hashed);
  return base64encoded;
};

export const tokenIsExpired = (): boolean => {
  const accessToken = localStorage.getItem("id_token");

  if (!accessToken) return true;

  const decodedToken: IdToken = jwtDecode(accessToken);

  const expirationTime = decodedToken.exp;
  const currentDate = new Date();
  const currentTime = currentDate.getTime() / 1000;

  return expirationTime < currentTime;
};

export const generatePKCE = async (): Promise<PKCE> => {
  const codeVerifier = generateCodeVerifier();
  const codeChallenge = await generateCodeChallenge(codeVerifier);

  return {
    codeChallenge,
    codeVerifier,
  };
};
