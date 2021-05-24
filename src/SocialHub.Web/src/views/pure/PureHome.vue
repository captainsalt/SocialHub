<template>
  <PostContainer :posts="posts"/>
</template>

<script lang="ts">
import PostContainer from "@/components/PostContainer.vue";

export default {
  components: {
    PostContainer
  },
  props: {
    posts: {
      type: Array,
      required: true
    }
  }
};
</script>
