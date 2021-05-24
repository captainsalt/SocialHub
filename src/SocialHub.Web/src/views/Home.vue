<template>
  <PureHome :posts="posts"/>
</template>

<script lang="ts">
import PureHome from "@/views/pure/PureHome.vue";
import { usePostsStore } from "@/store/local";

export default {
  components: { PureHome },
  setup() {
    const { posts } = usePostsStore();

    return {
      posts
    };
  }
};
</script>

