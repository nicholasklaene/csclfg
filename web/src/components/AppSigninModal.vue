<script setup lang="ts">
import AppModalBase from "./AppModalBase.vue";
import { defineEmits } from "vue";
const emit = defineEmits(["close"]);

function close() {
  emit("close");
}
</script>

<template>
  <AppModalBase @close="close()">
    <template v-slot:content>
      <div>content!</div>
    </template>
  </AppModalBase>
</template>
