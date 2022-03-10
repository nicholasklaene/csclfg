<script setup lang="ts">
import { useCategoryStore } from "@/stores/category";
const categoryStore = useCategoryStore();
</script>

<template>
  <div class="bg-backgroundCompliment px-4 pt-4 pb-2">
    <form>
      <div class="row gx-4 gy-2">
        <div class="col-12 col-md-6">
          <select class="form-control text-text border-background py-2">
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
          <select class="form-control text-text border-background py-2">
            <option :value="1">Past Hour</option>
            <option :value="24">Past Day</option>
            <option :value="24 * 7">Past Week</option>
            <option :value="24 * 31">Past Month</option>
            <option :value="-1">All Time</option>
          </select>
        </div>
        <div class="col-6">
          <button class="btn btn-primary w-100">Go!</button>
        </div>
        <div class="col-12">
          <p class="text-primary fw-bolder" role="button">Advanced Search</p>
        </div>
      </div>
    </form>
  </div>
</template>
