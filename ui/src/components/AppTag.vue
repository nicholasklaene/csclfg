<script setup lang="ts">
interface Props {
  tag: string;
  removeable: boolean;
}

defineProps<Props>();
defineEmits(["remove"]);
</script>

<template>
  <span class="bg-blue-300 text-gray-900 rounded-md px-2">
    <span>
      {{ tag }}
    </span>
    <span
      v-if="removeable"
      class="font-bold text-lg ml-2 hover:cursor-pointer"
      @click="$emit('remove')"
    >
      x
    </span>
  </span>
</template>
