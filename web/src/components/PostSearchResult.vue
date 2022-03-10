<script setup lang="ts">
import { defineProps } from "vue";
import { Post } from "@/types/post";

interface Props {
  post: Post;
}

const props = defineProps<Props>();
</script>

<template>
  <div>{{ post }}</div>
</template>
