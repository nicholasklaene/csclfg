import { RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";
import Authenticated from "../views/Authenticated.vue";
import OAuthCallback from "../views/OAuthCallback.vue";

export const routes: RouteRecordRaw[] = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/authenticated",
    name: "Authenticated",
    component: Authenticated,
  },
  {
    path: "/oauth/callback",
    name: "OAuthCallback",
    component: OAuthCallback,
  },
];
