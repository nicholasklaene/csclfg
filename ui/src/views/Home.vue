<script setup lang="ts">
import { onMounted } from "vue";
import { usePostStore } from "../stores/postStore";
import AppNavbar from "../components/AppNavbar.vue";
import AppProgressBar from "../components/AppProgressBar.vue";
import SearchResultList from "../components/SearchResultList.vue";
import SearchBox from "../components/SearchBox.vue";
import { useCategoryStore } from "../stores/categoryStore";

const version: string = window.location.host.split(".")[0];

const postStore = usePostStore();
const categoryStore = useCategoryStore();

onMounted(async () => {
  const promises = [postStore.getPosts(), categoryStore.getCategories()];
  await Promise.all(promises);
});
</script>

<template>
  <AppProgressBar v-if="postStore.loading" class="absolute" />
  <AppNavbar />
  <div class="py-8 mx-4">
    <div class="mb-10 mx-auto flex gap-6 items-center justify-center">
      <img src="../assets/logo.png" />
      <h1 class="text-4xl text-blue-400 font-bold tracking-widest">
        {{ version }}
        <br />
        Name TBD
      </h1>
    </div>
    <main class="max-w-screen-lg mx-auto text-white">
      <SearchBox class="mb-4" />
      <SearchResultList />
    </main>
  </div>
</template>
