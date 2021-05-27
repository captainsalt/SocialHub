import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Landing from "@/views/Landing.vue";
import NavLayout from "@/views/NavLayout.vue";
import Home from "@/views/Home.vue";
import Profile from "@/views/Profile.vue";
import { token } from "@/store";

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

router.beforeEach(async (to, from, next) => {
  if (!to.matched.some(record => record.meta.requiresAuth))
    return next();

  if (!token.value)
    return next({ path: "/"})

  next();
})

export default router;
