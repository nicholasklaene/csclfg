import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "./assets/theme.scss";

document.title = `${window.location.href.split(".")[0]} | Study Seeking`;

createApp(App).use(router).mount("#app");
