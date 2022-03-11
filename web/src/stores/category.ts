import { defineStore } from "pinia";
import { Category } from "@/types/category";
import { getApiUrl } from "@/utils";
import axios from "axios";

const baseUrl = getApiUrl();

export const useCategoryStore = defineStore("category", {
  state: () => ({
    loading: false,
    categories: [] as Category[],
  }),
  actions: {
    async getCategories() {
      this.setLoading(true);
      const result = await axios.get(`${baseUrl}/categories`);
      this.categories = result.data.categories;
      this.setLoading(false);
    },
    setLoading(value: boolean) {
      this.loading = value;
    },
  },
});
