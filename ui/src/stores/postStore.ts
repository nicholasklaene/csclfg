import { defineStore } from "pinia";
import { InitialPostStoreState, PostStoreState } from "../types/post";
import { baseUrl } from "../config";
import axios from "axios";

export const usePostStore = defineStore("post", {
  state: (): PostStoreState => InitialPostStoreState,
  actions: {
    async getPosts() {
      this.loading = true;

      const searchParams = new URLSearchParams();

      searchParams.append("category", this.search.category);

      if (this.search.limit) {
        searchParams.append("limit", this.search.limit.toString());
      }

      if (this.search.start) {
        searchParams.append("start", this.search.start.toString());
      }

      const searchUrl = `${baseUrl}/posts?${searchParams.toString()}`;

      const response = await axios.get(searchUrl);

      if (response.status === 200) {
        this.posts = response.data.posts;
      }

      this.loading = false;
    },
  },
});
