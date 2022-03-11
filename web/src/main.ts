import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

// TODO: include correct theme based on site
import "./assets/theme.scss";

import { getApp } from "./utils";
import { useInterceptors } from "./config/interceptors";

const pinia = createPinia();

document.title = `${getApp()} | Study Seeking`;

useInterceptors();

createApp(App).use(pinia).use(router).mount("#app");
