<template>
  <div>
    <keep-alive>
      <component :is="view" v-bind="dynamicArgs"/>
    </keep-alive>

    <BottomNav class="fixed bottom-0 w-screen" @on-nav="changeView"/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import { ref, shallowRef } from "vue";
import PureHome from "@/views/pure/PureHome.vue";
import PureProfile from "@/views/pure/PureProfile.vue";
import BottomNav from "@/components/BottomNav.vue";
import { account } from "@/store";
import { usePostsStore } from "@/store/local";

const { posts: homePosts } = usePostsStore();
const { posts: profilePosts } = usePostsStore();
const view = shallowRef(PureHome);
const dynamicArgs = ref({});

function changeView(payload: string) {
  switch (payload) {
    case "home":
      view.value = PureHome;
      dynamicArgs.value = {
        posts: homePosts
      };
      break;
    case "profile":
      view.value = PureProfile;
      dynamicArgs.value = {
        posts: profilePosts,
        account
      };
      break;
  }
}
</script>
