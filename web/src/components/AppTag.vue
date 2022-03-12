<script setup lang="ts">
import { defineProps, defineEmits } from "vue";

interface Props {
  label: string;
  removeable?: boolean;
}

const props = defineProps<Props>();

const emit = defineEmits(["remove"]);
</script>

<template>
  <span class="text-primaryText bg-primary px-2 py-1 rounded">
    <span>{{ label }}</span>
    <span
      v-if="props.removeable"
      class="fw-bold"
      role="button"
      @click="emit('remove')"
    >
      &nbsp;X
    </span>
  </span>
</template>
