import { usePostStore } from "@/stores/post";
import { Search } from "@/types/search";

export function getYear() {
  return new Date().getFullYear();
}

export function getApp() {
  return window.location.href.split(".")[0];
}

export function getApiUrl() {
  const baseDomain = "studyseeking.com";
  let app = getApp();
  if (app.includes("localhost")) app = "test";
  return `https://api.${app}.${baseDomain}`;
}

export function hoursSinceLastPost() {
  const postStore = usePostStore();

  const lastPostSeconds =
    postStore.searchResults.slice(-1)[0].created_at * 1000;

  const lastPostTime = new Date(lastPostSeconds).getTime();

  return Math.floor((new Date().getTime() - lastPostTime) / 1000 / 60 / 60);
}

export function timeSincePost(createdAt: number) {
  const postTime = new Date(createdAt * 1000).getTime();

  const minutes = Math.floor((new Date().getTime() - postTime) / 1000 / 60);

  if (minutes < 60) return `${minutes}m`;

  if (minutes < 60 * 24) {
    const hours = Math.floor(minutes / 60);
    return `${hours}h`;
  }

  const days = Math.floor(minutes / (60 * 24));

  return `${days}d`;
}

export function getOAuthServerUrl() {
  return "https://identity.studyseeking.com";
}

export function getOAuthClientId() {
  return "2lmskmbukto86mhvvqs666bime";
}

export function getOAuthCallback() {
  let callbackUrl: string;
  if (getApp().includes("localhost")) {
    callbackUrl = "http://localhost:8080/oauth/callback";
  } else {
    callbackUrl = `https://${getApp()}.studyseeking.com/oauth/callback`;
  }
  return callbackUrl;
}

export function getDefaultSearch(): Search {
  return {
    end: -1,
    category: "Algorithms",
  };
}
