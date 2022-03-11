import { RouteRecordRaw } from "vue-router";
import HomeView from "../views/HomeView.vue";
import OAuthCallback from "@/views/utils/OAuthCallback.vue";

export const routes: Array<RouteRecordRaw> = [
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
