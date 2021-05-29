<template>
  <div>
    <div class="divide-y rounded-md">
      <Post
        v-for="p in posts"
        :key="p.id"
        :post="p"
      />
    </div>
    <div id="offset" class="h-36"/>
  </div>
</template>

<script lang="ts">
import Post from "@/components/Post.vue";

export default {
  components: {
    Post
  },
  props: {
    posts: {
      type: Array,
      required: true
    }
  }
};
</script>
