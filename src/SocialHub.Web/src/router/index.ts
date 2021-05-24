import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Landing from "@/views/Landing.vue";
import NavLayout from "@/views/NavLayout.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "landing",
    component: Landing
  },
  {
    path: "/home",
    name: "home",
    meta: { requiresAuth: true },
    component: NavLayout
  },
  {
    path: "/profile",
    name: "profile",
    meta: { requiresAuth: true },
    component: NavLayout
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
