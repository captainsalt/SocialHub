<template>
  <div>
    <router-view/>

    <div ref="bottom" class="fixed bottom-0 w-screen">
      <PostInput v-show="isHome" class="px-4 mb-2"/>
      <BottomNav ref="nav" @on-nav="changeView"/>
    </div>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { onMounted, ref, watch } from "vue";
import router from "@/router";
import BottomNav from "@/components/BottomNav.vue";
import PostInput from "@/components/PostInput.vue";

const isHome = ref(router.currentRoute.value.name === "Home");

watch(() => router.currentRoute.value.name, val => {
  isHome.value = router.currentRoute.value.name === "Home";
});

async function changeView(payload: string) {
  await router.push(payload);
}
</script>
