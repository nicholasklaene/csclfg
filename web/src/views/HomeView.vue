<script setup lang="ts">
import PostSearchBox from "@/components/PostSearchBox.vue";
import PostSearchHeader from "@/components/PostSearchHeader.vue";
import PostSearchResults from "../components/PostSearchResults.vue";
const application = window.location.href.split(".")[0];
</script>

<template>
  <div class="row mt-4 gy-3">
    <div class="col-12">
      <div class="d-flex">
        <img src="../assets/logo.png" alt="logo" />
        <h1 class="ms-4 align-self-center text-secondary">
          {{ application }}
          <br />
          Study Seeking
        </h1>
      </div>
    </div>
    <div class="col-12">
      <PostSearchHeader />
    </div>
    <div class="col-12">
      <PostSearchBox />
    </div>
    <div class="col-12">
      <PostSearchResults />
    </div>
  </div>
</template>
