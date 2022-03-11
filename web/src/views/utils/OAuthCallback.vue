<script setup lang="ts">
import { onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAuthStore } from "@/stores/auth";

const route = useRoute();
const router = useRouter();
const authStore = useAuthStore();

onMounted(async () => {
  const code = route.query.code?.toString();
  const randomString = route.query.state?.toString();

  if (!code || !randomString) router.push("/");

  await authStore.exchangeCodeForAccessToken(
    String(code?.toString()),
    String(randomString?.toString())
  );

  router.push(`/`);
});
</script>

<template>
  <p class="text-white text-center my-4">
    Please wait to be redirected...
    <br />
    <button class="btn p-0">
      <router-link to="/">
        If you are not automatically redirected, click here.
      </router-link>
    </button>
  </p>
</template>
