<template>
  <div>
    <router-view/>
    <BottomNav class="fixed bottom-0 w-screen" @on-nav="changeView"/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { shallowRef, onBeforeMount } from "vue";
import router from "@/router";
import BottomNav from "@/components/BottomNav.vue";

async function changeView(payload: string) {
  await router.push(payload);
}
</script>
