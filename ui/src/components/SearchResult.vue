<script setup lang="ts">
import { Post } from "../types/post";
import AppTag from "./AppTag.vue";

interface Props {
  post: Post;
}

const props = defineProps<Props>();

const formattedDescription = () =>
  props.post.description.length < 300
    ? props.post.description
    : `${props.post.description.substring(0, 300)} ...`;

const timeSincePost = () => {
  const minutes = Math.floor(
    (new Date().getTime() - new Date(props.post.created_at * 1000).getTime()) /
      1000 /
      60
  );

  if (minutes < 60) return `${minutes}m`;

  if (minutes < 60 * 24) {
    const hours = Math.floor(minutes / 60);
    return `${hours}h`;
  }

  const days = Math.floor(minutes / (60 * 24));
  return `${days}d`;
};
</script>

<template>
  <div class="border-b border-collapse border-gray-500 px-4 py-6">
    <div class="flex">
      <h3 class="text-xl w-[90%]">
        <router-link to="/">
          {{ post.title }}
        </router-link>
      </h3>
      <p class="text-right ml-auto w-[10%]">
        {{ timeSincePost() }}
      </p>
    </div>
    <div class="my-2">
      <p>
        {{ formattedDescription() }}
      </p>
    </div>
    <div class="flex gap-2 mt-2">
      <AppTag
        v-for="tag in post.tags"
        :key="tag"
        :tag="tag"
        :removeable="false"
      />
    </div>
  </div>
</template>
