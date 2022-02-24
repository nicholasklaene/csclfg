<script setup lang="ts">
import { onMounted, onUnmounted, Ref, ref } from "vue";
import { usePostStore } from "../stores/postStore";
import AppProgressBar from "../components/AppProgressBar.vue";
import SearchResultList from "../components/SearchResultList.vue";
import SearchBox from "../components/SearchBox.vue";
import { useCategoryStore } from "../stores/categoryStore";
import { useAuthStore } from "../stores/authStore";

const postStore = usePostStore();
const categoryStore = useCategoryStore();
const authStore = useAuthStore();

onMounted(async () => {
  const promises = [postStore.getPosts(), categoryStore.getCategories()];
  await Promise.all(promises);
});
</script>

<template>
  <AppProgressBar v-if="postStore.loading" class="absolute" />
  <div class="py-8 mx-4">
    <button
      class="bg-blue-500 text-white"
      @click="authStore.redirectToAuthServer(true)"
    >
      Login
    </button>
    <div class="mb-2">
      <img src="../assets/logo.jpg" class="mx-auto mb-8" />
    </div>
    <main class="max-w-screen-lg mx-auto bg-gray-800 text-white min-h-[800px]">
      <SearchBox />
      <SearchResultList />
    </main>
  </div>
</template>
