<script setup lang="ts">
import { Ref, ref } from "vue";
import { usePostStore } from "../stores/postStore";
import { useCategoryStore } from "../stores/categoryStore";
import AppTag from "./AppTag.vue";

const postStore = usePostStore();
const categoryStore = useCategoryStore();

const tag = ref("");
const tags: Ref<string[]> = ref([]);

const handleAddTag = () => {
  if (!tag.value.trim()) return;
  tags.value.push(tag.value.toLowerCase());
  tag.value = "";
};

const handleRemoveTag = (tag: string) => {
  tags.value = tags.value.filter((t) => t !== tag);
};

const handleSubmit = () => {
  console.log("Submitted!");
};
</script>

<template>
  <form
    @submit.prevent="handleSubmit()"
    class="flex flex-col gap-4 pt-4 max-w-[95%] mx-auto"
  >
    <h1 class="text-3xl">Create Post</h1>
    <label>
      <p class="text-gray-400">Title*</p>
      <input type="text" class="bg-gray-900 text-white p-2 w-full" />
    </label>
    <label>
      <p class="text-gray-400">Category*</p>
      <select class="bg-gray-900 p-2 w-full">
        <option :value="-1" disabled selected hidden></option>
        <option
          v-for="category in categoryStore.categories"
          :value="category.label"
        >
          {{ category.label }}
        </option>
      </select>
    </label>
    <label>
      <p class="text-gray-400">Post information*</p>
      <textarea class="bg-gray-900 text-white p-2 w-full" rows="10"></textarea>
    </label>
    <label>
      <p class="text-gray-400">
        Add up to 5 tags to make your post easier to find...
      </p>
      <input
        type="text"
        class="bg-gray-900 text-white p-2 w-full"
        placeholder="tag name..."
        :disabled="tags.length >= 5"
        v-model="tag"
        @keyup.enter="handleAddTag()"
      />
      <div class="flex gap-2 mt-4">
        <AppTag
          v-for="tag in tags"
          :key="tag"
          :tag="tag"
          :removeable="true"
          @remove="handleRemoveTag(tag)"
        />
      </div>
    </label>
    <button
      class="border border-blue-300 bg-blue-300 hover:opacity-90 py-2 px-8 text-gray-900 md:max-w-[50%]"
    >
      Create
    </button>
  </form>
</template>
