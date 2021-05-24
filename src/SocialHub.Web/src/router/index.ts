import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Landing from "@/views/Landing.vue";
import Dashboard from "@/views/Dashboard.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Landing",
    component: Landing
  },
  {
    path: "/dashboard",
    name: "dashboard",
    meta: { requiresAuth: true },
    component: Dashboard
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
