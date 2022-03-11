import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { useAuthStore } from "@/stores/auth";
import HomeView from "../views/HomeView.vue";
import OAuthCallback from "@/views/utils/OAuthCallback.vue";
import { useUserStore } from "@/stores/user";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
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
  const userStore = useUserStore();

  let isAuthenticated = authStore.authenticationCheck();
  isAuthenticated = isAuthenticated && authStore.state.isAuthenticated;

  if (isAuthenticated) await userStore.createUser();

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
