import { usePostStore } from "@/stores/post";
import { Post } from "@/types/post";

export function getApp() {
  return window.location.href.split(".")[0];
}

export function getApiUrl() {
  const baseDomain = "studyseeking.com";
  const app = getApp();
  return `https://${app}.api.${baseDomain}`;
}

export function hoursSinceLastPost() {
  const postStore = usePostStore();

  const lastPostSeconds =
    postStore.searchResults.slice(-1)[0].created_at * 1000;

  const lastPostTime = new Date(lastPostSeconds).getTime();

  return Math.floor((new Date().getTime() - lastPostTime) / 1000 / 60 / 60);
}

export function timeSincePost(post: Post) {
  const postTime = new Date(post.created_at * 1000).getTime();

  const minutes = Math.floor((new Date().getTime() - postTime) / 1000 / 60);

  if (minutes < 60) return `${minutes}m`;

  if (minutes < 60 * 24) {
    const hours = Math.floor(minutes / 60);
    return `${hours}h`;
  }

  const days = Math.floor(minutes / (60 * 24));

  return `${days}d`;
}

export function formatPostDescription(post: Post) {
  return post.description.length < 300
    ? post.description
    : `${post.description.substring(0, 300)} ...`;
}
