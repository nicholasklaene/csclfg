<script setup lang="ts">
import { usePostStore } from "@/stores/post";
import PostSearchResult from "./PostSearchResult.vue";
const postStore = usePostStore();
</script>

<template>
  <div class="bg-backgroundCompliment pt-4 pb-2">
    <div
      class="d-flex flex-column gap-4 pb-4"
      v-if="postStore.searchResults.length > 0"
    >
      <PostSearchResult
        v-for="(post, index) in postStore.searchResults"
        :key="post.post_id"
        :post="post"
        :class="{
          'border-bottom pb-4': index !== postStore.searchResults.length - 1,
        }"
      />
    </div>
    <p class="text-center" v-else>
      Doesn't look like we found anything... try widening your search criteria.
    </p>
  </div>
</template>
