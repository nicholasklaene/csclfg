import { usePostStore } from "../stores/postStore";
import { baseUrl } from "../config";

export default function (): string {
  const postStore = usePostStore();

  const searchParams = new URLSearchParams();

  searchParams.append("category", postStore.search.category);
  searchParams.append("end", postStore.search.end.toString());

  if (postStore.search.limit) {
    searchParams.append("limit", postStore.search.limit.toString());
  }

  if (postStore.search.start) {
    searchParams.append("start", postStore.search.start.toString());
  }

  return `${baseUrl}/posts?${searchParams.toString()}`;
}
