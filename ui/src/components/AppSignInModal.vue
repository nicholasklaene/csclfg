<script setup lang="ts">
import AppButton from "./buttons/AppButton.vue";
import AppOutlinedButton from "./buttons/AppOutlinedButton.vue";
import AppModalBase from "./AppModalBase.vue";

const emit = defineEmits(["close"]);
const handleClose = () => emit("close");
</script>

<template>
  <AppModalBase @close="handleClose()">
    <template v-slot:title>
      <span> You must sign in to continue.</span>
    </template>
    <template v-slot:content>
      <AppButton> Sign in </AppButton>
      <AppOutlinedButton> Sign up </AppOutlinedButton>
    </template>
  </AppModalBase>
</template>
