<script setup lang="ts">
import AppModalBase from "./AppModalBase.vue";
import AppSigninButton from "./buttons/AppSigninButton.vue";
import AppSignoutButton from "./buttons/AppSignoutButton.vue";

const emit = defineEmits(["close"]);
const handleClose = () => emit("close");
</script>

<template>
  <AppModalBase @close="handleClose()">
    <template v-slot:title>
      <span> You must sign in to continue.</span>
    </template>
    <template v-slot:content>
      <AppSigninButton />
      <AppSignoutButton />
    </template>
  </AppModalBase>
</template>
