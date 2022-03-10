<script setup lang="ts">
import AppSigninModal from "./AppSigninModal.vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/auth";
import { ref } from "vue";

const authStore = useAuthStore();
const router = useRouter();

const showSignInModal = ref<boolean>(false);

function handleCreate() {
  authStore.authenticationCheck();
  if (authStore.state.isAuthenticated) {
    router.push("/create-post");
  } else {
    toggleSignInModal(true);
  }
}

function toggleSignInModal(value: boolean) {
  showSignInModal.value = value;
}
</script>

<template>
  <AppSigninModal v-if="showSignInModal" @close="toggleSignInModal(false)" />
  <div class="bg-backgroundCompliment px-4 pt-4 pb-2">
    <div class="d-flex">
      <h3>Find a Group</h3>
      <div class="ms-auto">
        <button class="btn btn-outline-primary px-4" @click="handleCreate()">
          Create
        </button>
      </div>
    </div>
  </div>
</template>
