import { defineStore } from "pinia";
import { Post, CreatePost } from "@/types/post";
import { getApiUrl, hoursSinceLastPost, getDefaultSearch } from "@/utils";
import axios from "axios";

const baseUrl = getApiUrl();

export const usePostStore = defineStore("post", {
  state: () => ({
    loading: false,
    reachedEnd: false,
    search: getDefaultSearch(),
    searchResults: [] as Post[],
    postCache: new Map<string, Post>(),
  }),
  actions: {
    async getPostById(postId: string) {
      this.setLoading(true);

      const searchUrl = `${baseUrl}/posts/${postId}`;
      const response = await axios.get(searchUrl).catch((error) => {
        alert("Not found!");
      });

      if (response && response.status === 200) {
        const post = response.data.post;
        this.postCache.set(postId, post);
      }

      this.setLoading(false);
    },

    async getPosts() {
      if (this.loading) return;

      this.setLoading(true);

      this.search.start = undefined;
      this.reachedEnd = false;

      const searchUrl = this.buildSearch();
      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.searchResults = response.data.posts;
        if (this.searchResults.length < 10) this.reachedEnd = true;
      }

      this.setLoading(false);
    },

    async getMorePosts() {
      if (
        this.loading ||
        this.searchResults.length === 0 ||
        this.reachedEnd ||
        (hoursSinceLastPost() > this.search.end && this.search.end !== -1)
      ) {
        return;
      }

      this.setLoading(true);
      this.search.start = this.searchResults.slice(-1)[0].created_at;

      const searchUrl = this.buildSearch();

      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.searchResults = [...this.searchResults, ...response.data.posts];
        if (response.data.posts.length < 10) this.reachedEnd = true;
      }

      this.setLoading(false);
    },

    async createPost(data: CreatePost) {
      this.setLoading(true);

      const requestUrl = `${baseUrl}/posts`;
      const response = await axios.post(requestUrl, data);

      if (response.status !== 201) return false;

      this.setLoading(false);

      return true;
    },
    buildSearch() {
      const searchParams = new URLSearchParams();

      searchParams.append("category", this.search.category);

      if (this.search.limit) {
        searchParams.append("limit", this.search.limit.toString());
      }

      if (this.search.start) {
        searchParams.append("start", this.search.start.toString());
      }

      if (this.search.tags && this.search.tags.length > 0) {
        searchParams.append("tags", this.search.tags.join(","));
      }

      if (this.search.end != -1) {
        const endDate = new Date();
        const distanceBack = this.search.end * 60 * 60 * 1000;
        endDate.setTime(endDate.getTime() - distanceBack);

        const adjustedEnd = (endDate.getTime() / 1000).toString();
        searchParams.append("end", adjustedEnd);
      }

      return `${baseUrl}/posts?${searchParams.toString()}`;
    },
    setLoading(value: boolean) {
      this.loading = value;
    },
  },
});
