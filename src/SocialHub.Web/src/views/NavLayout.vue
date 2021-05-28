<template>
  <div>
    <router-view/>

    <div ref="bNav" class="fixed bottom-0 w-screen">
      <PostInput
        v-show="isHome"
        class="px-4 mb-2"
        @error="setOffsetHeight"
      />
      <BottomNav @on-nav="changeView"/>
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
