import { createApp } from "vue";
import App from "./App.vue";
import router from "./router/index";
import { createPinia } from "pinia";
import { setHttpInterceptors } from "./config/axios";
import "./index.css";

setHttpInterceptors();

createApp(App).use(router).use(createPinia()).mount("#app");
