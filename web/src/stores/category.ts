import { ref } from "vue";
import { defineStore } from "pinia";
import { Category } from "@/types/category";
import { getApiUrl } from "@/utils";
import axios from "axios";

const baseUrl = getApiUrl();

export const useCategoryStore = defineStore("category", () => {
  const loading = ref<boolean>(false);
  const categories = ref<Category[]>();

  async function getCategories() {
    setLoading(true);
    const result = await axios.get(`${baseUrl}/categories`);
    categories.value = result.data.categories;
    setLoading(false);
  }

  function setLoading(value: boolean) {
    loading.value = value;
  }

  return {
    getCategories,
    setLoading,
  };
});
