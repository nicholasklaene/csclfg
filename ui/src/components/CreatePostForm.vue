<script setup lang="ts">
import { Ref, ref } from "vue";
import {
  CreatePostForm,
  CreatePostFormInitialValue,
} from "../types/forms/createPost";

import { usePostStore } from "../stores/postStore";
import { useCategoryStore } from "../stores/categoryStore";
import { validate } from "../formValidation/createPost";
import AppTag from "./AppTag.vue";

const postStore = usePostStore();
const categoryStore = useCategoryStore();

const tag: Ref<string> = ref("");
const formData: CreatePostForm = CreatePostFormInitialValue();

const handleAddTag = (): void => {
  if (!tag.value.trim()) return;
  formData.tags.value.push(tag.value.toLowerCase());
  tag.value = "";
};

const handleRemoveTag = (tag: string): void => {
  formData.tags.value = formData.tags.value.filter((t) => t !== tag);
};

const handleSubmit = async (): Promise<void> => {
  if (!validate(formData)) return;
  console.log("valid");
};
</script>

<template>
  <form
    @submit.prevent="handleSubmit()"
    class="flex flex-col gap-1 pt-4 pb-8 max-w-[95%] mx-auto"
  >
    <h1 class="text-3xl mb-4">Create Post</h1>
    <label>
      <p class="text-gray-400">Title*</p>
      <input
        type="text"
        class="bg-gray-900 text-white p-2 w-full"
        required
        v-model="formData.title.value"
      />
      <small
        class="text-red-500"
        :class="formData.title.error ? 'visible' : 'invisible'"
      >
        Title must be between 1 and 100 characters
      </small>
    </label>
    <label>
      <p class="text-gray-400">Category*</p>
      <select
        class="bg-gray-900 p-2 w-full"
        required
        v-model="formData.category.value"
      >
        <option value="-1" disabled selected hidden></option>
        <option
          v-for="category in categoryStore.categories"
          :value="category.label"
        >
          {{ category.label }}
        </option>
      </select>
      <small
        class="text-red-500"
        :class="formData.category.error ? 'visible' : 'invisible'"
      >
        Category is required
      </small>
    </label>
    <label>
      <p class="text-gray-400">Post information*</p>
      <small class="text-gray-400">
        Make sure to include details on how to join
      </small>
      <textarea
        class="bg-gray-900 text-white p-2 w-full"
        rows="10"
        required
        v-model="formData.description.value"
      ></textarea>
      <small
        class="text-red-500"
        :class="formData.description.error ? 'visible' : 'invisible'"
      >
        Description must be between 1 and 1024 characters
      </small>
    </label>
    <label>
      <p class="text-gray-400">
        Add up to 5 tags to make your post easier to find
      </p>
      <input
        type="text"
        class="bg-gray-900 text-white p-2 w-full"
        placeholder="tag name"
        :disabled="formData.tags.value.length >= 5"
        v-model="tag"
        @keyup.enter="handleAddTag()"
      />
      <div class="flex gap-2 my-4">
        <AppTag
          v-for="tag in formData.tags.value"
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
