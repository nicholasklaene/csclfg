<script setup lang="ts">
import { usePostStore } from "@/stores/post";
import PostSearchResult from "./PostSearchResult.vue";
const postStore = usePostStore();
</script>

<template>
  <div class="bg-backgroundCompliment px-4 pt-4 pb-2">
    <div class="text-center" v-if="postStore.searchResults.length > 0">
      <PostSearchResult
        v-for="post in postStore.searchResults"
        :key="post.post_id"
        :post="post"
      />
    </div>
    <p v-else>
      Doesn't look like we found anything... try widening your search criteria.
    </p>
  </div>
</template>
