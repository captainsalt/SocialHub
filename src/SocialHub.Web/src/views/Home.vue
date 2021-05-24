<template>
  <div>
    <keep-alive>
      <component :is="view" v-bind="args"/>
    </keep-alive>

    <BottomNav class="fixed bottom-0 w-screen" @on-nav="changeView"/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { shallowRef } from "vue";
import PureHome from "@/views/pure/PureHome.vue";
import PureProfile from "@/views/pure/PureProfile.vue";
import BottomNav from "@/components/BottomNav.vue";
import { account } from "@/store";

const view = shallowRef(PureHome);

function changeView(payload: string) {
  switch (payload) {
    case "home":
      view.value = PureHome;
      break;
    case "profile":
      view.value = PureProfile;
      break;
  }
}

const args = {
  posts: [],
  account
};
</script>
