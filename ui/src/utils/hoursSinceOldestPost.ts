import { usePostStore } from "../stores/postStore";

// yuck
export default function () {
  const postStore = usePostStore();
  return Math.floor(
    (new Date().getTime() -
      new Date(
        postStore.posts[postStore.posts.length - 1].created_at * 1000
      ).getTime()) /
      1000 /
      60 /
      60
  );
}
