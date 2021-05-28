import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import Button from "@/components/Button.vue";
import "./assets/tailwind.css";

const app = createApp(App);

app.component("Button", Button);

app.use(router)
  .mount("#app");
