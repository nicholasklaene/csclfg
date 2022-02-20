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
      this.reachedEnd = false;

      const searchUrl = buildSearch();
      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.posts = response.data.posts;
      }

      this.loading = false;
    },
    async getMorePosts() {
      if (
        this.posts.length === 0 ||
        this.reachedEnd ||
        hoursSinceOldestPost() > this.search.end
      ) {
        return;
      }

      this.loading = true;
      this.search.start = this.posts[this.posts.length - 1].created_at;

      const searchUrl = buildSearch();

      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.posts = [...this.posts, ...response.data.posts];
        if (response.data.posts.length === 0) this.reachedEnd = true;
      }

      this.loading = false;
    },
  },
});
