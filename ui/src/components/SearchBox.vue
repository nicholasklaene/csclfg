<script setup lang="ts">
import { defaultCategory } from "../config";
import { useCategoryStore } from "../stores/categoryStore";
import { usePostStore } from "../stores/postStore";
import SearchBarHeader from "./SearchBarHeader.vue";
import AppButton from "./buttons/AppButton.vue";

const categoryStore = useCategoryStore();
const postStore = usePostStore();
</script>

<template>
  <div class="border-b-[1rem] border-gray-900 py-6">
    <SearchBarHeader />
    <div class="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-3 px-4">
      <select class="bg-gray-900 p-2" v-model="postStore.search.category">
        <option v-if="categoryStore.loading" :value="defaultCategory">
          {{ defaultCategory }}
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
      <select class="bg-gray-900 p-2" v-model="postStore.search.end">
        <option :value="1">Past Hour</option>
        <option :value="24">Past Day</option>
        <option :value="24 * 7">Past Week</option>
        <option :value="24 * 31">Past Month</option>
        <option :value="-1">All Time</option>
      </select>
      <AppButton @click="postStore.getPosts()" :disabled="postStore.loading">
        Go!
      </AppButton>
    </div>
    <div class="mt-1 px-4">
      <a href="#" class="text-blue-500 tracking-wider">Add tags to search</a>
    </div>
  </div>
</template>
