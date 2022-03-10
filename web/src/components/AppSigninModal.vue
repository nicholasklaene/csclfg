<script setup lang="ts">
import AppModalBase from "./AppModalBase.vue";
import AppSigninButton from "./AppSigninButton.vue";
import AppSignupButton from "./AppSignupButton.vue";
import { defineEmits } from "vue";

const emit = defineEmits(["close"]);

function close() {
  emit("close");
}
</script>

<template>
  <AppModalBase @close="close()">
    <template v-slot:content>
      <div class="px-2">
        <h4 class="text-center">You'll need to sign in to post</h4>
        <div class="row gy-2">
          <AppSigninButton />
          <AppSignupButton />
        </div>
      </div>
    </template>
  </AppModalBase>
</template>
