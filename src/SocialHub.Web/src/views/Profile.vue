<template>
  <PureProfile :account="account" :posts="posts"/>
</template>

<script lang="ts">
import PureProfile from "@/views/pure/PureProfile.vue";
import { account } from "@/store";
import { usePostsStore } from "@/store/local";

export default {
  components: { PureProfile },
  setup() {
    const { posts } = usePostsStore();

    return {
      posts,
      account
    };
  }
};
</script>
