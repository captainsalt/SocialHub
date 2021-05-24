<template>
  <div class="divide-y rounded-md">
    <Post
      v-for="p in posts"
      :key="p.id"
      :post="p"
    />
  </div>
</template>

<script lang="ts">
import Post from "@/components/Post.vue";
import PostModel from "@/models/PostModel";

export default {
  components: {
    Post
  },
  props: {
    posts: {
      type: Array,
      required: true,
      validator: (val: any[]) =>
        val.every(item => item instanceof PostModel)
    }
  }
};
</script>
