import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "./assets/theme.scss";
import { getApp } from "./utils";

const pinia = createPinia();
document.title = `${getApp()} | Study Seeking`;

createApp(App).use(pinia).use(router).mount("#app");
