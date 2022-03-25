<script setup lang="ts">
import {onMounted} from "vue";
import {useApplicationStore} from "./stores/applicationStore";

const applicationStore = useApplicationStore();

onMounted(() => {
  applicationStore.getApplications();
})
</script>

<template>
  <v-app>
    <v-main>
      <h1>Hello, world!</h1>
      {{ applicationStore.applications }}
    </v-main>
  </v-app>
</template>
