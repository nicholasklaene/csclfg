<script setup lang="ts">
import { defineEmits } from "vue";

const emit = defineEmits(["close"]);

function close() {
  emit("close");
}
</script>

<template>
  <div
    class="position-fixed sticky-top top-0 start-0 h-100 w-100 opacity-75 bg-overlay"
    @click.self="close()"
  ></div>

  <div class="modal-dialog position-fixed">
    <div class="modal-content bg-backgroundCompliment text-text">
      <div class="modal-body">
        <slot name="content"></slot>
      </div>
    </div>
  </div>
</template>

<style scoped>
div.modal-dialog {
  z-index: 9999;
  left: 50%;
  transform: translate(-50%, 0);
  width: 320px;
}
</style>
