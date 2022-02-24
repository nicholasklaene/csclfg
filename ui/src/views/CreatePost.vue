<script setup lang="ts">
import AppProgressBar from "../components/AppProgressBar.vue";
import AppNavbar from "../components/AppNavbar.vue";
import CreatePostForm from "../components/CreatePostForm.vue";
import { onMounted } from "vue";
import { useCategoryStore } from "../stores/categoryStore";

const categoryStore = useCategoryStore();

onMounted(async () => {
  if (categoryStore.categories.length === 0) {
    await categoryStore.getCategories();
  }
});
</script>

<template>
  <AppProgressBar v-if="categoryStore.loading" class="absolute max-w-[95%]" />
  <AppNavbar />
  <div class="py-8 mx-4 flex">
    <main
      class="max-w-screen-lg w-screen mx-auto lg:mt-8 bg-gray-800 text-white"
    >
      <CreatePostForm />
    </main>
  </div>
</template>
