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
  <div class="py-8 mx-4">
    <main class="max-w-screen-lg mx-auto bg-gray-800 text-white">
      <CreatePostForm />
    </main>
  </div>
</template>
