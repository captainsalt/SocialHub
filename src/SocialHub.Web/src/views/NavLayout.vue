<template>
  <div>
    <router-view/>

    <div ref="bNav" class="fixed bottom-0 w-screen">
      <PostInput v-show="isHome" class="px-4 mb-2"/>
      <BottomNav @on-nav="changeView"/>
    </div>
    <div ref="offsetDiv"/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { onMounted, ref, Ref, watch } from "vue";
import router from "@/router";
import BottomNav from "@/components/BottomNav.vue";
import PostInput from "@/components/PostInput.vue";

const bNav: Ref<HTMLDivElement | null> = ref(null);
const offsetDiv: Ref<HTMLDivElement | null> = ref(null);

function setOffsetHeight(value: string | number) {
  offsetDiv.value.style.height = `${value}px`;
}

async function changeView(payload: string) {
  await router.push(payload);
  setOffsetHeight(bNav.value?.offsetHeight);
}

onMounted(() => {
  setOffsetHeight(bNav.value?.offsetHeight);
});

const isHome = ref(router.currentRoute.value.name === "Home");
watch(() => router.currentRoute.value.name, newVal => {
  isHome.value = newVal === "Home";
});
</script>
