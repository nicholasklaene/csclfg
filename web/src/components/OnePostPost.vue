<script setup lang="ts">
import { defineProps } from "vue";
import { Post } from "@/types/post";
import { marked } from "marked";

interface Props {
  post: Post;
}

const props = defineProps<Props>();
</script>

<template>
  <div class="bg-backgroundCompliment mt-auto">
    <div class="mb-2 border-2 border-bottom border-background pt-4 px-4 pb-2">
      <h5>
        <span>User: </span>
        <router-link class="text-text" :to="`/users/${props.post.user_id}`">
          {{ props.post.user_id }}
        </router-link>
      </h5>
    </div>
    <div
      class="pt-2 px-4 pb-4"
      v-html="marked.parse(props.post.description)"
    ></div>
  </div>
</template>
