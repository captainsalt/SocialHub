<template>
  <div class="h-screen">
    <PostContainer :posts="posts"/>
    <BottomNav/>
  </div>
</template>

<script lang="ts">
import PostContainer from "@/components/PostContainer.vue";
import BottomNav from "@/components/BottomNav.vue";

export default {
  components: {
    PostContainer,
    BottomNav
  },
  props: {
    posts: {
      type: Array,
      required: true
    }
  }
};
</script>
