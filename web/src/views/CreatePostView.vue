<script setup lang="ts">
import CreatePostForm from "@/components/CreatePostForm.vue";
</script>

<template>
  <main class="mx-auto px-4 my-4 mw-100">
    <div>
      <CreatePostForm />
    </div>
  </main>
</template>

<style>
main > div {
  max-width: 1024px;
}
</style>
