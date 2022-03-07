import { createRouter, createWebHistory } from "vue-router";
import { routes } from "./routes";
import { useAuthStore } from "../stores/authStore";

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore();
  const isAuthenticated = authStore._isAuthenticated;
  if (to.meta.requiresAuth && !isAuthenticated) {
    const success = await authStore.refresh();
    if (success) {
      next();
    } else {
      next({ name: "Home" });
    }
  } else {
    next();
  }
});

export default router;
