<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";
import { usePostStore } from "@/stores/post";
import PostSearchResult from "./PostSearchResult.vue";
const postStore = usePostStore();

onMounted(() => {
  window.addEventListener("scroll", handleScroll);
});

onUnmounted(() => {
  window.removeEventListener("scroll", handleScroll);
});

const scrollComponent = ref<HTMLElement | null>(null);
const handleScroll = (e: Event) => {
  let element = scrollComponent.value!;
  if (element.getBoundingClientRect().bottom < window.innerHeight) {
    if (!postStore.loading) postStore.getMorePosts();
  }
};
</script>

<template>
  <div class="bg-backgroundCompliment pt-4 pb-2 mb-4" ref="scrollComponent">
    <div
      class="d-flex flex-column gap-4 pb-4"
      v-if="postStore.searchResults.length > 0"
    >
      <PostSearchResult
        v-for="post in postStore.searchResults"
        :key="post.post_id"
        :post="post"
        class="border-bottom pb-4"
      />
      <p class="text-center px-4" v-if="postStore.reachedEnd">
        You've reached the end... try widening your search criteria
      </p>
    </div>
    <p class="text-center px-4" v-else>
      Doesn't look like we found anything... try widening your search criteria.
    </p>
  </div>
</template>
