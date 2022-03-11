import { useAuthStore } from "@/stores/auth";
import { useUserStore } from "@/stores/user";
import { NavigationGuardNext, RouteLocationNormalized } from "vue-router";

export const beforeEach = async (
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
) => {
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
};
