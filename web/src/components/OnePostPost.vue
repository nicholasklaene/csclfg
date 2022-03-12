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
  <div class="bg-backgroundCompliment p-4 mt-auto">
    <div v-html="marked.parse(props.post.description)"></div>
  </div>
</template>
