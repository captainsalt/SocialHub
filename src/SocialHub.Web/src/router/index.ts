import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Landing from "@/views/Landing.vue";
import NavLayout from "@/views/NavLayout.vue";
import Home from "@/views/Home.vue";
import Profile from "@/views/Profile.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Landing",
    component: Landing
  },
  {
    path: "/home",
    name: "Layout",
    meta: { requiresAuth: true },
    component: NavLayout,
    children: [
      { path: "", name: "Home", component: Home },
      { path: "/:username", name: "Profile", component: Profile }
    ]
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
