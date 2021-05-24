<template>
  <div>
    <keep-alive>
      <component :is="view"/>
    </keep-alive>

    <BottomNav class="fixed bottom-0 w-screen" @on-nav="changeView"/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { shallowRef, onBeforeMount } from "vue";
import router from "@/router";
import Home from "@/views/Home.vue";
import Profile from "@/views/Profile.vue";
import BottomNav from "@/components/BottomNav.vue";

const view = shallowRef(Home);

onBeforeMount(() => {
  const route = router.currentRoute.value.name;
  changeView(route);
});

function changeView(payload: string) {
  router.push(payload);

  switch (payload) {
    case "home":
      view.value = Home;
      break;
    case "profile":
      view.value = Profile;
      break;
  }
}
</script>
