import { RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";
import CreatePost from "../views/CreatePost.vue";
import OAuthCallback from "../views/OAuthCallback.vue";
import PersonalProfile from "../views/PersonalProfile.vue";
import Post from "../views/Post.vue";
import NotFound from "../views/NotFound.vue";

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
    path: "/posts/:id",
    name: "Post",
    component: Post,
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
  {
    path: "/404",
    name: "404",
    component: NotFound,
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
