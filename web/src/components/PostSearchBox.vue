<script setup lang="ts">
import { ref } from "vue";
import { useCategoryStore } from "@/stores/category";
import { usePostStore } from "@/stores/post";
import AppTag from "./AppTag.vue";
const categoryStore = useCategoryStore();
const postStore = usePostStore();

const showAdvancedSearch = ref(false);

function toggleAdvancedSearch() {
  showAdvancedSearch.value = !showAdvancedSearch.value;
}

function addTagToSearch(tag: string) {
  postStore.search.tags
    ? postStore.search.tags.push(tag)
    : (postStore.search.tags = [tag]);
}

function removeTagFromSearch(tag: string) {
  postStore.search.tags = postStore.search.tags?.filter((t) => t !== tag);
}

function suggestedTags() {
  return categoryStore.categories.find(
    (c) => c.label === postStore.search.category
  )?.suggested_tags;
}
</script>

<template>
  <div class="bg-backgroundCompliment px-4 pt-4 pb-2">
    <form @submit.prevent="postStore.getPosts()">
      <div class="row gx-4 gy-2">
        <div class="col-12 col-md-6">
          <select
            class="form-control text-text border-background py-2"
            v-model="postStore.search.category"
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
        <div class="col-12 col-md-6">
          <select
            class="form-control text-text border-background py-2"
            v-model="postStore.search.end"
          >
            <option :value="1">Past Hour</option>
            <option :value="24">Past Day</option>
            <option :value="24 * 7">Past Week</option>
            <option :value="24 * 31">Past Month</option>
            <option :value="-1">All Time</option>
          </select>
        </div>
        <div class="col-6">
          <button
            class="btn btn-primary text-primaryText w-100"
            :class="{ disabled: postStore.loading }"
          >
            Go!
          </button>
        </div>

        <div class="col-12" v-if="showAdvancedSearch">
          <h5>Contains one of these tags:</h5>
          <div
            class="mb-2"
            v-if="!postStore.search.tags || postStore.search.tags?.length === 0"
            role="text"
          >
            <span> no tags selected... </span>
          </div>
          <div class="d-flex flex-wrap gap-2 mb-2" v-else>
            <template v-for="tag in postStore.search.tags" :key="tag">
              <AppTag
                :label="tag"
                :removeable="true"
                @remove="removeTagFromSearch(tag)"
              />
            </template>
          </div>

          <h6 class="mb-2" role="text">Suggested tags:</h6>
          <div class="d-flex flex-wrap gap-2 mb-2">
            <template v-for="tag in suggestedTags()" :key="tag">
              <AppTag
                v-if="!postStore.search.tags?.includes(tag)"
                :label="tag"
                role="button"
                @click="addTagToSearch(tag)"
              />
            </template>
          </div>
        </div>

        <div class="col-12">
          <p
            class="text-primary fw-bolder"
            role="button"
            @click="toggleAdvancedSearch()"
          >
            Toggle Advanced Search
          </p>
        </div>
      </div>
    </form>
  </div>
</template>
