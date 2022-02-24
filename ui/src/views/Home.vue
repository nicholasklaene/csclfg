<script setup lang="ts">
import { onMounted } from "vue";
import { usePostStore } from "../stores/postStore";
import AppNavbar from "../components/AppNavbar.vue";
import AppProgressBar from "../components/AppProgressBar.vue";
import SearchResultList from "../components/SearchResultList.vue";
import SearchBox from "../components/SearchBox.vue";
import { useCategoryStore } from "../stores/categoryStore";

const postStore = usePostStore();
const categoryStore = useCategoryStore();

onMounted(async () => {
  const promises = [postStore.getPosts(), categoryStore.getCategories()];
  await Promise.all(promises);
});
</script>

<template>
  <AppNavbar />
  <div class="py-8 mx-4">
    <div class="mb-2">
      <img src="../assets/logo.jpg" class="mx-auto mb-8" />
    </div>
    <main class="max-w-screen-lg mx-auto bg-gray-800 text-white min-h-[800px]">
      <AppProgressBar v-if="postStore.loading" class="absolute" />
      <SearchBox />
      <SearchResultList />
    </main>
  </div>
</template>
