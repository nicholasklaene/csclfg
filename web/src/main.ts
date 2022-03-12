import App from "@/App.vue";
import router from "@/router";
import { createApp } from "vue";

import "@/config/title";
import "@/config/axios";
import "@/config/bootstrap";
import FontAwesomeIcon from "@/config/fontawesome";
import pinia from "@/config/pinia";

createApp(App)
  .component("font-awesome-icon", FontAwesomeIcon)
  .use(pinia)
  .use(router)
  .mount("#app");
