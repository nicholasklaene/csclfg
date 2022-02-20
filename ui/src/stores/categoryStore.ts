import { defineStore } from "pinia";
import axios from "axios";
import { baseUrl } from "../config";
import { InitialCategoryStoreState } from "../types/category";

export const useCategoryStore = defineStore("category", {
  state: () => InitialCategoryStoreState,
  actions: {
    async getCategories() {
      this.loading = true;
      const result = await axios.get(`${baseUrl}/categories`);
      this.categories = result.data.categories;
      this.loading = false;
    },
  },
});
