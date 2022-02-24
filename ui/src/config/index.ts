export const baseUrl = String(import.meta.env.VITE_API_BASE_URL!);
export const clientId = String(import.meta.env.VITE_CLIENT_ID!);
export const callbackUrl = String(import.meta.env.VITE_CALLBACK_URL!);
export const authServerBaseURL = String(import.meta.env.VITE_AUTH_SERVER!);

export const defaultCategory = "Algorithms";

// 1, 24, 24 * 7, 24 * 31, -1
export const defaultSearchTimeSpanHours = 24;
