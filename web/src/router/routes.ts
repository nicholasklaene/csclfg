import { RouteRecordRaw } from "vue-router";
import HomeView from "@/views/HomeView.vue";
import CreatePostView from "@/views/CreatePostView.vue";
import OnePostView from "@/views/OnePostView.vue";
import OAuthCallback from "@/views/utils/OAuthCallback.vue";
import NotFound from "@/views/utils/NotFound.vue";

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
    path: "/create-post",
    name: "create-post",
    component: CreatePostView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/posts/:id",
    name: "OnePostView",
    component: OnePostView,
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
  {
    path: "/:catchAll(.*)",
    name: "NotFound",
    component: NotFound,
    meta: {
      requiresAuth: false,
    },
  },
];
