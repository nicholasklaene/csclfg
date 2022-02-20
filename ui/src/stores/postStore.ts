import { defineStore } from "pinia";
import { InitialPostStoreState, PostStoreState } from "../types/post";
import axios from "axios";

const baseUrl = "https://08spkh2273.execute-api.us-east-1.amazonaws.com";

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
