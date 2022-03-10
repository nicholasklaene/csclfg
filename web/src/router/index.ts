import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { useAuthStore } from "@/stores/auth";
import HomeView from "../views/HomeView.vue";
import OAuthCallback from "@/views/utils/OAuthCallback.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: HomeView,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/oauth/callback",
    name: "OAuthCallback",
    component: OAuthCallback,
    meta: {
      requiresAuth: false,
    },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore();

  let isAuthenticated = authStore.authenticationCheck();
  isAuthenticated = isAuthenticated && authStore.state.isAuthenticated;

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
