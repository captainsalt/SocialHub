<template>
  <div class="w-full border-2 border-purple-700 divide-y rounded-md">
    <Post v-for="p in posts" :key="p.id" :post="p"/>
  </div>
</template>

<script lang="ts">
import Post from "@/components/Post.vue";
import PostModel from "@/models/PostModel";
import { PropType } from "vue"

export default {
  components: {
    Post
  },
  props: {
    posts: {
      type: Array as PropType<Array<PostModel>>,
      required: true,
      validator: (val: PostModel[]) =>
        val.every(item => item instanceof PostModel)
    }
  }
};
</script>
