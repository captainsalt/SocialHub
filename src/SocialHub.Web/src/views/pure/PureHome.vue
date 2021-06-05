<template>
  <div>
    <div class="sm:hidden">
      <img class="h-10 mx-auto" src="@/assets/logo.png">
    </div>
    <PostContainer :posts="posts"/>
  </div>
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
