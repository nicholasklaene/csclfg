<script setup lang="ts">
import { defineProps } from "vue";
import { truncatePostDescription, timeSincePost } from "@/utils";
import { Post } from "@/types/post";
import AppTag from "@/components/AppTag.vue";

interface Props {
  post: Post;
}

const props = defineProps<Props>();
</script>

<template>
  <div class="px-4">
    <div class="d-flex">
      <router-link
        class="text-text align-self-start text-decoration-none"
        :to="`/posts/${post.post_id}`"
      >
        <h5>{{ post.title }}</h5>
      </router-link>
      <p class="ms-auto">{{ timeSincePost(post.created_at) }}</p>
    </div>
    <h6>Category: {{ post.category }}</h6>
    <p>{{ post.preview }}</p>
    <div class="d-flex gap-2">
      <AppTag v-for="tag in post.tags" :key="tag" :label="tag" />
    </div>
  </div>
</template>
