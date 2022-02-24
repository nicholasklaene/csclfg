<script setup lang="ts">
import { useRouter } from "vue-router";
import { useAuthStore } from "../stores/authStore";

const authStore = useAuthStore();
const router = useRouter();

const handleCreateClick = () => {
  if (authStore.isAuthenticated) {
    router.push({ name: "CreatePost" });
  }
};
</script>

<template>
  <div class="flex mb-4 border-b-[1rem] border-b-gray-900 px-4 pb-4">
    <h2 class="text-2xl font-bold">Find a Group</h2>
    <button
      class="ml-auto border border-blue-500 text-blue-500 py-2 px-8 hover:opacity-80"
      @click="handleCreateClick()"
    >
      Create
    </button>
  </div>
</template>
