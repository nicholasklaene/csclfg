<script setup lang="ts">
import PostSearchBox from "@/components/PostSearchBox.vue";
import PostSearchHeader from "@/components/PostSearchHeader.vue";
import PostSearchResults from "../components/PostSearchResults.vue";
import { getApp } from "@/utils";
import { onMounted } from "vue";
import { useCategoryStore } from "@/stores/category";
import { usePostStore } from "@/stores/post";
import AppProgressBar from "@/components/AppProgressBar.vue";

const categoryStore = useCategoryStore();
const postStore = usePostStore();
const application = getApp().includes("localhost")
  ? "TEST"
  : getApp().toLocaleUpperCase();

onMounted(() => {
  if (categoryStore.categories.length === 0) {
    categoryStore.getCategories();
  }

  if (postStore.searchResults.length === 0) {
    postStore.getPosts();
  }
});
</script>

<template>
  <main class="mx-auto px-4">
    <div class="row mt-4 gy-3">
      <div class="col-12">
        <div class="d-flex">
          <img class="me-4" src="../assets/logo.png" alt="logo" />
          <h1 class="align-self-center text-primary">
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
        <AppProgressBar v-if="postStore.loading || categoryStore.loading" />
        <PostSearchBox />
      </div>
      <div class="col-12">
        <PostSearchResults />
      </div>
    </div>
  </main>
</template>
