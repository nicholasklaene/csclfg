<script setup lang="ts">
import { onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAuthStore } from "../stores/authStore";

onMounted(async () => {
  const route = useRoute();
  const router = useRouter();
  const authStore = useAuthStore();

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
  <p class="text-white">
    Please wait to be redirected...
    <br />
    <button text>
      <router-link to="/">
        If you are not automatically redirected, click here.
      </router-link>
    </button>
  </p>
</template>
