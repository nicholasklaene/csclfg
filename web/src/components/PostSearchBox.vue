<script setup lang="ts">
import { Ref, ref } from "vue";

interface Category {
  label: string;
}

interface TimeSpan {
  label: string;
  value: number;
}

const categories: Ref<Category[]> = ref([
  {
    label: "Algorithms",
  },
  {
    label: "System Design",
  },
]);

const timespans: Ref<TimeSpan[]> = ref([
  { label: "Past hour", value: 60 * 60 },
  { label: "Past day", value: 60 * 60 * 24 },
  { label: "Past month", value: 60 * 60 * 24 * 30 },
  { label: "All time", value: -1 },
]);
</script>

<template>
  <div class="bg-backgroundCompliment px-4 pt-4 pb-2">
    <form>
      <div class="row gx-4 gy-2">
        <div class="col-12 col-md-6">
          <select class="form-control text-text border-background py-2">
            <template v-for="category in categories" :key="category.label">
              <option :value="category.label">
                {{ category.label }}
              </option>
            </template>
          </select>
        </div>
        <div class="col-12 col-md-6">
          <select class="form-control text-text border-background py-2">
            <template v-for="timespan in timespans" :key="timespan.value">
              <option :value="timespan.value">
                {{ timespan.label }}
              </option>
            </template>
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
