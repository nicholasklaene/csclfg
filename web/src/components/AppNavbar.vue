<script setup lang="ts">
import AppSigninButton from "./AppSigninButton.vue";
import AppSignupButton from "./AppSignupButton.vue";
import AppSignoutButton from "./AppSignoutButton.vue";
import { ref } from "vue";

const isLoggedIn = ref(false);
</script>

<template>
  <nav class="navbar navbar-expand navbar-light bg-light">
    <div class="container-fluid">
      <router-link to="/" class="navbar-brand">Study Seeking</router-link>
      <div class="navbar-collapse">
        <ul class="navbar-nav ms-auto">
          <template v-if="isLoggedIn">
            <li class="nav-item">
              <AppSignoutButton />
            </li>
          </template>
          <template v-else>
            <li class="nav-item me-2">
              <AppSigninButton />
            </li>
            <li class="nav-item">
              <AppSignupButton />
            </li>
          </template>
        </ul>
      </div>
    </div>
  </nav>
</template>
