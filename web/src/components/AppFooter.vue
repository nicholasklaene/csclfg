<script setup lang="ts">
import { getYear } from "@/utils";
</script>

<template>
  <footer
    class="text-center text-lg-start bg-backgroundCompliment text-muted px-2"
  >
    <section class="pt-5">
      <div class="container text-center text-md-start">
        <div class="row mt-3">
          <div class="col-md-4 mx-auto mb-4 d-flex">
            <div class="mx-auto">
              <h6 class="text-uppercase fw-bold mb-4">Study Seeking</h6>
              <p>
                Studying in a small group deepens learning. Find yours today.
              </p>
            </div>
          </div>
          <div class="col-md-4 mx-auto mb-4 d-flex">
            <div class="mx-auto">
              <h6 class="text-uppercase fw-bold mb-4">Links</h6>
              <p>
                <a
                  href="https://studyapps.com/apps"
                  target="__blank"
                  class="text-reset"
                >
                  Study Apps
                </a>
              </p>
              <p>
                <a
                  href="https://studyseeking.com/about"
                  class="text-reset"
                  target="__blank"
                >
                  About
                </a>
              </p>
              <p>
                <a
                  href="https://studyseeking.com/report"
                  class="text-reset"
                  target="__blank"
                >
                  Report a bug
                </a>
              </p>
            </div>
          </div>
          <div class="col-md-4 mx-auto mb-4 d-flex">
            <div class="mx-auto">
              <h6 class="text-uppercase fw-bold mb-4">Contact</h6>
              <p>
                <font-awesome-icon :icon="['fa', 'envelope']" class="me-2" />
                <span>admin@studyseeking.com</span>
              </p>
            </div>
          </div>
        </div>
      </div>
    </section>
    <div class="text-center p-4" style="background-color: rgba(0, 0, 0, 0.05)">
      &copy; {{ getYear() }} Copyright:
      <a
        class="text-reset text-decoration-none fw-bold"
        href="https://studyseeking.com/"
        target="__blank"
      >
        studyseeking.com
      </a>
    </div>
  </footer>
</template>
