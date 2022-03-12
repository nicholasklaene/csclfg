<script setup lang="ts">
import { usePostStore } from "@/stores/post";
import { useCategoryStore } from "@/stores/category";
import { onMounted, ref } from "vue";
import AppMarkdownEditor from "./AppMarkdownEditor.vue";

const postDescription = ref("");
const postStore = usePostStore();
const categoryStore = useCategoryStore();

function submit() {
  console.log(postDescription.value);
}

onMounted(async () => {
  if (categoryStore.categories.length === 0) {
    await categoryStore.getCategories();
  }
});

const bindDescription = (value: string) => (postDescription.value = value);
</script>

<template>
  <form
    class="bg-backgroundCompliment w-100 mw-100 p-4"
    @submit.prevent="submit()"
  >
    <div class="row g-3">
      <h2>Create a Post</h2>
      <div class="col-12">
        <label class="mb-1" for="category">Title</label>
        <div>
          <input
            class="bg-background w-100 text-text"
            type="text"
            placeholder="My Group Name"
            required
          />
        </div>
      </div>
      <div class="col-12">
        <label class="mb-1" for="category">Category</label>
        <select
          class="form-control text-text border-background py-2"
          v-model="postStore.search.category"
          required
        >
          <template
            v-for="category in categoryStore.categories"
            :key="category.label"
          >
            <option :value="category.label">
              {{ category.label }}
            </option>
          </template>
        </select>
      </div>
      <div class="col-12">
        <label class="mb-1" for="category">Preview Text</label>
        <div>
          <input
            class="bg-background w-100 text-text"
            type="text"
            placeholder="Preview text for search results page"
            required
          />
        </div>
      </div>
      <div class="col-12">
        <label class="mb-1" for="category">Post content</label>
        <AppMarkdownEditor @input="bindDescription" />
      </div>
      <div class="col-12 col-md-6">
        <button class="btn btn-primary text-text w-100">Submit</button>
      </div>
    </div>
  </form>
</template>
