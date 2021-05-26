<template>
  <div class="divide-y rounded-md">
    <Post
      v-for="p in posts"
      :key="p.id"
      :post="p"
      @like="like"
      @share="share"
      @like-remove="removeLike"
      @share-remove="removeShare"
    />
  </div>
</template>

<script lang="ts">
import Post from "@/components/Post.vue";
import PostModel from "@/models/PostModel";
import { share, like, removeLike, removeShare } from "@/services/api-interface";

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
  },
  setup() {
    return {
      share,
      like,
      removeLike,
      removeShare
    };
  }
};
</script>
