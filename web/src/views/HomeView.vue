<script setup lang="ts">
import PostSearchBox from "@/components/PostSearchBox.vue";
import PostSearchHeader from "@/components/PostSearchHeader.vue";
import PostSearchResults from "../components/PostSearchResults.vue";
import { getApp } from "@/utils";
import { onMounted } from "vue";
import { useCategoryStore } from "@/stores/category";
import { usePostStore } from "@/stores/post";

const categoryStore = useCategoryStore();
const postStore = usePostStore();
const application = getApp().includes("localhost") ? "test" : getApp();

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
  </main>
</template>
