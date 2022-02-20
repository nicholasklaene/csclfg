import { defineStore } from "pinia";
import { InitialPostStoreState, PostStoreState } from "../types/post";
import buildSearch from "../utils/buildSearch";
import hoursSinceOldestPost from "../utils/hoursSinceOldestPost";
import axios from "axios";

export const usePostStore = defineStore("post", {
  state: (): PostStoreState => InitialPostStoreState,
  actions: {
    async getPosts() {
      this.loading = true;

      this.search.start = undefined;
      const searchUrl = buildSearch();
      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.posts = response.data.posts;
      }

      this.loading = false;
    },
    // Infinite scroll - the above completely replaces posts
    async getMorePosts() {
      if (this.posts.length === 0) return;
      if (hoursSinceOldestPost() > this.search.end) return;

      this.loading = true;
      this.search.start = this.posts[this.posts.length - 1].created_at;

      const searchUrl = buildSearch();

      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.posts = [...this.posts, ...response.data.posts];
      }

      this.loading = false;
    },
  },
});
