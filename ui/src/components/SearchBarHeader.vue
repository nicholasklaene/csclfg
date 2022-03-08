<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "../stores/authStore";
import AppOutlinedButton from "./buttons/AppOutlinedButton.vue";
import AppSignInModal from "./AppSignInModal.vue";

const authStore = useAuthStore();
const router = useRouter();

const showModal = ref(true);

const handleCreateClick = () => {
  if (authStore._isAuthenticated) {
    router.push({ name: "CreatePost" });
  } else {
    showModal.value = true;
  }
};

const handleModalClose = () => (showModal.value = false);
</script>

<template>
  <div class="flex mb-4 border-b-[1rem] border-b-gray-900 px-4 pb-4">
    <h2 class="text-2xl font-bold">Find a Group</h2>
    <AppOutlinedButton class="ml-auto" @click="handleCreateClick()">
      Create
    </AppOutlinedButton>
  </div>
  <AppSignInModal v-if="showModal" @close="handleModalClose()" />
</template>
