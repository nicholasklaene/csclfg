import { RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";
import CreatePost from "../views/CreatePost.vue";
import OAuthCallback from "../views/OAuthCallback.vue";
import PersonalProfile from "../views/PersonalProfile.vue";

export const routes: RouteRecordRaw[] = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/create-post",
    name: "CreatePost",
    component: CreatePost,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/profile",
    name: "PersonalProfile",
    component: PersonalProfile,
    meta: {
      requiresAuth: true,
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
