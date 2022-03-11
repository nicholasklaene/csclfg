import { reactive, ref } from "vue";
import { defineStore } from "pinia";
import { Post, CreatePost } from "@/types/post";
import { getDefaultSearch, Search } from "@/types/search";
import { getApiUrl, hoursSinceLastPost } from "@/utils";
import axios from "axios";

const baseUrl = getApiUrl();

// Probably want this to be a traditonal store for dev tools
export const usePostStore = defineStore("post", () => {
  const loading = ref<boolean>(false);
  const reachedEnd = ref<boolean>(false);
  const search = reactive<Search>(getDefaultSearch());
  const searchResults = ref<Post[]>([]);
  const postCache = new Map<string, Post>();

  function buildSearch() {
    const searchParams = new URLSearchParams();

    searchParams.append("category", search.category);

    if (search.limit) searchParams.append("limit", search.limit.toString());

    if (search.start) searchParams.append("start", search.start.toString());

    if (search.tags) searchParams.append("tags", search.tags.join(","));

    if (search.end != -1) {
      const endDate = new Date();
      const distanceBack = search.end * 60 * 60 * 1000;
      endDate.setTime(endDate.getTime() - distanceBack);

      const adjustedEnd = (endDate.getTime() / 1000).toString();
      searchParams.append("end", adjustedEnd);
    }

    return `${baseUrl}/posts?${searchParams.toString()}`;
  }

  async function getPostById(postId: string) {
    setLoading(true);

    const searchUrl = `${baseUrl}/posts/${postId}`;
    const response = await axios.get(searchUrl).catch((e) => {
      //
    });
    if (!response) return;

    if (response.status === 200) {
      const post = response.data.post;
      postCache.set(postId, post);
    }

    setLoading(false);
  }

  async function getPosts() {
    if (loading.value) return;

    setLoading(true);

    search.start = undefined;
    reachedEnd.value = false;

    const searchUrl = buildSearch();
    const response = await axios.get(searchUrl);

    if (response.status === 200) {
      searchResults.value = response.data.posts;
    }

    setLoading(false);
  }

  async function getMorePosts() {
    if (
      loading.value ||
      searchResults.value.length === 0 ||
      reachedEnd.value ||
      hoursSinceLastPost() > search.end
    ) {
      return;
    }

    setLoading(true);
    search.start = searchResults.value.slice(-1)[0].created_at;

    const searchUrl = buildSearch();

    const response = await axios.get(searchUrl);

    if (response.status === 200) {
      searchResults.value = [...searchResults.value, ...response.data.posts];
      if (response.data.posts.length === 0) reachedEnd.value = true;
    }

    setLoading(false);
  }

  async function createPost(data: CreatePost) {
    setLoading(true);

    const requestUrl = `${baseUrl}/posts`;
    const response = await axios.post(requestUrl, data);

    if (response.status !== 201) return false;

    setLoading(false);

    return true;
  }

  function setLoading(value: boolean) {
    loading.value = value;
  }

  return {
    loading,
    reachedEnd,
    search,
    searchResults,
    postCache,
    getPostById,
    getPosts,
    getMorePosts,
    createPost,
    setLoading,
  };
});
