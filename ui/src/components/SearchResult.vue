<script setup lang="ts">
import { Post } from "../types/post";

interface Props {
  post: Post;
}

const props = defineProps<Props>();

const formattedDescription = () =>
  props.post.description.length < 300
    ? props.post.description
    : `${props.post.description.substring(0, 300)} ...`;
</script>

<template>
  <h3 class="text-xl">
    <router-link to="/">
      {{ post.title }}
    </router-link>
  </h3>
  <div class="my-2">
    <p>
      {{ formattedDescription() }}
    </p>
  </div>
  <div class="flex gap-2 mt-2">
    <span
      v-for="tag in post.tags"
      :key="tag"
      class="bg-blue-400 rounded-md px-2"
    >
      {{ tag }}
    </span>
  </div>
</template>
