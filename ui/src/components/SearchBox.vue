<script setup lang="ts">
import { useCategoryStore } from "../stores/categoryStore";

const categoryStore = useCategoryStore();
</script>

<template>
  <div class="border-b-[1rem] border-gray-900 px-4 py-6">
    <div class="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3">
      <select class="bg-gray-900 p-2">
        <option v-if="categoryStore.loading" :value="categoryStore.default">
          {{ categoryStore.default }}
        </option>
        <option
          v-else
          v-for="category in categoryStore.categories"
          :key="category.label"
          :value="category.label"
        >
          {{ category.label }}
        </option>
      </select>
      <select class="bg-gray-900 p-2">
        <option>Past 24 Hours</option>
      </select>
      <button class="bg-blue-300 hover:opacity-90 p-2 text-gray-900">
        Go!
      </button>
    </div>
    <div class="mt-1">
      <a href="#" class="text-blue-500 tracking-wider">Add tags to search</a>
    </div>
  </div>
</template>
