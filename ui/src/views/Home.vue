<script setup lang="ts">
import { onMounted } from "vue";
import { usePostStore } from "../stores/postStore";
import AppProgressBar from "../components/AppProgressBar.vue";
import SearchResultList from "../components/SearchResultList.vue";
import SearchBox from "../components/SearchBox.vue";

const postStore = usePostStore();

onMounted(async () => {
  await postStore.getPosts();
});
</script>

<template>
  <AppProgressBar v-if="postStore.loading" class="absolute" />
  <div class="py-8 mx-8">
    <div class="mb-2">
      <img src="../assets/logo.jpg" class="mx-auto mb-8" />
    </div>
    <main class="max-w-screen-lg mx-auto bg-gray-800 text-white min-h-[800px]">
      <SearchBox />
      <SearchResultList />
    </main>
  </div>
</template>
