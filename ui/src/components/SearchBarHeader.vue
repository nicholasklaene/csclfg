<script setup lang="ts">
import { useRouter } from "vue-router";
import { useAuthStore } from "../stores/authStore";
import AppOutlinedButton from "./buttons/AppOutlinedButton.vue";

const authStore = useAuthStore();
const router = useRouter();

const handleCreateClick = () => {
  if (authStore._isAuthenticated) {
    router.push({ name: "CreatePost" });
  }
};
</script>

<template>
  <div class="flex mb-4 border-b-[1rem] border-b-gray-900 px-4 pb-4">
    <h2 class="text-2xl font-bold">Find a Group</h2>
    <AppOutlinedButton class="ml-auto" @click="handleCreateClick()">
      Create
    </AppOutlinedButton>
  </div>
</template>
