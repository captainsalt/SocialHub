<template>
  <div>
    <div class="p-1 sm:grid-flow-col sm:grid-cols-12 sm:gap-1 sm:grid">
      <SideNav class="hidden sm:col-span-3 sm:inline-block"/>
      <router-view class="sm:col-span-9 sm:inline-block"/>
    </div>

    <div ref="bNav" class="fixed bottom-0 w-screen">
      <PostInput
        v-show="isHome"
        class="px-4 mb-2"
        @error="setOffsetHeight"
      />
      <BottomNav class="sm:hidden" @on-nav="changeView"/>
    </div>
    <div ref="offsetDiv"/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { onMounted, ref, watch } from "vue";
import router from "@/router";
import BottomNav from "@/components/BottomNav.vue";
import PostInput from "@/components/PostInput.vue";
import SideNav from "@/components/SideNav.vue";

const bNav: Ref<HTMLDivElement> = ref(); // eslint-disable-line no-undef
const offsetDiv: Ref<HTMLDivElement> = ref(); // eslint-disable-line no-undef

function setOffsetHeight() {
  offsetDiv.value.style.height = `${bNav.value?.offsetHeight}px`;
}

async function changeView(payload: string) {
  await router.push(payload);
  setOffsetHeight();
}

onMounted(() => {
  setOffsetHeight();
});

const isHome = ref(router.currentRoute.value.name === "Home");

watch(router.currentRoute, newRoute => {
  isHome.value = newRoute.name === "Home";
});
</script>
