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
  <section ref="scrollComponent">
    <template v-if="postStore.posts.length > 0">
      <SearchResult
        v-for="post in postStore.posts"
        :key="post.post_id"
        :post="post"
      />
    </template>
    <p v-else class="text-center pt-4">
      Doesn't look like we found anything...
    </p>
  </section>
</template>
