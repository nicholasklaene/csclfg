<script setup lang="ts">
import { usePostStore } from "@/stores/post";
import { useCategoryStore } from "@/stores/category";
import { onMounted, reactive, ref } from "vue";
import AppMarkdownEditor from "./AppMarkdownEditor.vue";
import AppTag from "./AppTag.vue";

const createPost = reactive({
  title: "",
  category: "",
  preview: "",
  description: "",
  tags: [] as string[],
});

const postStore = usePostStore();
const categoryStore = useCategoryStore();

function submit() {
  //
}

function addTag(tag: string) {
  if (createPost.tags.length === 5) return;
  createPost.tags.push(tag);
}

function removeTag(tag: string) {
  createPost.tags = createPost.tags.filter((t) => t !== tag);
}

function suggestedTags() {
  try {
    if (categoryStore.loading) return [];
    return categoryStore.categories.find(
      (c) => c.label === createPost.category
    )!.suggested_tags;
  } catch (e) {
    return [];
  }
}

onMounted(async () => {
  if (categoryStore.categories.length === 0) {
    await categoryStore.getCategories();
  }
  createPost.category = categoryStore.categories[0].label;
});

const bindDescription = (value: string) => (createPost.description = value);
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
            v-model="createPost.title"
            required
          />
        </div>
      </div>
      <div class="col-12">
        <label class="mb-1" for="category">Category</label>
        <select
          class="form-control text-text border-background py-2"
          v-model="createPost.category"
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
        <label class="mb-1" for="preview">Preview Text</label>
        <div>
          <input
            class="bg-background w-100 text-text"
            type="text"
            placeholder="Preview text for search results page"
            v-model="createPost.preview"
            required
          />
        </div>
      </div>
      <div class="col-12">
        <label class="mb-1" for="content">Post content</label>
        <AppMarkdownEditor @input="bindDescription" />
      </div>
      <div class="col-12">
        <label class="mb-1" for="category">
          <div class="mb-1" role="text">
            Post tags:
            <span v-if="createPost.tags.length === 0"> no tags selected </span>
          </div>
          <div class="d-flex flex-wrap mb-2 gap-2">
            <template v-for="tag in createPost.tags" :key="tag">
              <AppTag
                :removeable="true"
                :label="tag"
                @remove="removeTag(tag)"
              />
            </template>
          </div>
          <div class="mb-1" role="text">Suggested tags:</div>
          <div class="d-flex flex-wrap mb-2 gap-2">
            <template v-for="tag in suggestedTags()" :key="tag">
              <AppTag
                v-if="!createPost.tags.includes(tag)"
                :label="tag"
                role="button"
                @click="addTag(tag)"
              />
            </template>
          </div>
          <div v-if="createPost.tags.length === 5" role="text">
            You may only select up to 5 tags
          </div>
        </label>
      </div>
      <div class="col-12 col-md-6">
        <button class="btn btn-primary text-text w-100">Submit</button>
      </div>
    </div>
  </form>
</template>
