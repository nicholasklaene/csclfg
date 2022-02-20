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
  <div class="py-16 mx-8">
    <main class="max-w-screen-lg mx-auto bg-gray-800 text-white min-h-[800px]">
      <SearchBox />
      <SearchResultList />
    </main>
  </div>
</template>
