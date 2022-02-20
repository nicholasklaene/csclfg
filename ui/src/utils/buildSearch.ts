import { usePostStore } from "../stores/postStore";
import { baseUrl } from "../config";

export default function (): string {
  const postStore = usePostStore();

  const searchParams = new URLSearchParams();

  searchParams.append("category", postStore.search.category);

  if (postStore.search.end != -1) {
    const endTs = new Date();
    endTs.setTime(
      endTs.getTime() + Number(-postStore.search.end) * 60 * 60 * 1000
    );
    searchParams.append("end", (endTs.getTime() / 1000).toString());
  }

  if (postStore.search.limit) {
    searchParams.append("limit", postStore.search.limit.toString());
  }

  if (postStore.search.start) {
    searchParams.append("start", postStore.search.start.toString());
  }

  return `${baseUrl}/posts?${searchParams.toString()}`;
}
