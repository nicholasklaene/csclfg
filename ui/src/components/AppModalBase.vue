<script setup lang="ts">
const emit = defineEmits(["close"]);
const handleClose = () => emit("close");
</script>

<template>
  <div
    class="bg-gray-900 opacity-80 fixed top-0 left-0 h-full w-full z-10"
    @click.self="handleClose()"
  ></div>

  <div
    class="modal fade fixed mx-auto my-auto inset-x-0 w-[400px] max-w-full outline-none overflow-x-hidden overflow-y-auto z-20"
  >
    <div
      class="modal-dialog modal-dialog-centered relative w-auto pointer-events-none"
    >
      <div
        class="modal-content border-none shadow-lg relative flex flex-col w-full pointer-events-auto bg-gray-800 bg-clip-padding rounded-md outline-none text-current"
      >
        <div class="modal-header p-4 rounded-t-md">
          <h5 class="text-xl font-medium leading-normal text-white text-center">
            <slot name="title"></slot>
          </h5>
        </div>
        <div class="modal-body relative flex flex-col gap-2 p-4">
          <slot name="content"></slot>
        </div>
      </div>
    </div>
  </div>
</template>
