<script setup lang="ts">
import { defineProps } from "vue";

interface Props {
  label: string;
}

const props = defineProps<Props>();
</script>

<template>
  <span class="text-primaryText bg-primary px-2 py-1 rounded">{{ label }}</span>
</template>
