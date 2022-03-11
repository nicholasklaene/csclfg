import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import { getApp } from "./utils";
import { useInterceptors } from "./config/interceptors";

try {
  //   const app = getApp();
  const app = "aws";
  require(`./assets/themes/${app}-theme.scss`);
} catch (e) {
  require("./assets/themes/theme.scss");
}

const pinia = createPinia();

document.title = `${getApp()} | Study Seeking`;

useInterceptors();

createApp(App).use(pinia).use(router).mount("#app");
