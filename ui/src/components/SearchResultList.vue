<script setup lang="ts">
import { onMounted, onUnmounted, Ref, ref } from "vue";
import { usePostStore } from "../stores/postStore";
import SearchResult from "./SearchResult.vue";

const postStore = usePostStore();
onMounted(() => {
  window.addEventListener("scroll", handleScroll);
});

onUnmounted(() => {
  window.removeEventListener("scroll", handleScroll);
});

const scrollComponent: Ref<HTMLElement | null> = ref(null);
const handleScroll = (e: Event) => {
  let element = scrollComponent.value!;
  if (element.getBoundingClientRect().bottom < window.innerHeight) {
    if (!postStore.loading) postStore.getMorePosts();
  }
};
</script>

<template>
  <section class="flex flex-col" ref="scrollComponent">
    <SearchResult
      v-for="post in postStore.posts"
      :key="post.post_id"
      :post="post"
    />
  </section>
</template>
