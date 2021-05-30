<template>
  <div class="sm:p-1">
    <div class="sm:grid-flow-col sm:grid-cols-12 sm:grid">
      <SideNav class="hidden sm:col-start-3 sm:block sm:sticky sm:top-0 sm:h-screen sm:col-span-2"/>
      <router-view class="sm:col-span-5"/>
    </div>

    <div class="fixed bottom-0 w-screen">
      <PostInput
        v-show="isHome"
        class="px-4 mb-2"
      />
      <BottomNav class="sm:hidden" @on-nav="changeView"/>
    </div>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { ref, watch } from "vue";
import router from "@/router";
import BottomNav from "@/components/BottomNav.vue";
import PostInput from "@/components/PostInput.vue";
import SideNav from "@/components/SideNav.vue";

async function changeView(payload: string) {
  await router.push(payload);
}

const isHome = ref(router.currentRoute.value.name === "Home");

watch(router.currentRoute, newRoute => {
  isHome.value = newRoute.name === "Home";
});
</script>
