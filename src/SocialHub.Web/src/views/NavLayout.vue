<template>
  <div>
    <div class="sm:grid-flow-col sm:grid-cols-12 sm:grid">
      <div class="hidden sm:flex sm:items-center sm:h-screen sm:col-span-3 sm:sticky sm:top-0">
        <SideNav class="w-full mx-1 overflow-hidden bg-purple-500 rounded-sm h-1/3"/>
      </div>
      <router-view class="sm:col-span-9"/>
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
