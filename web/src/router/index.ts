import { createRouter, createWebHistory } from "vue-router";
import { beforeEach } from "./beforeEach";
import { routes } from "./routes";

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach(beforeEach);

export default router;
