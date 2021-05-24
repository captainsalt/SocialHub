<template>
  <PureHome :posts="posts"/>
</template>

<script lang="ts">
import { onBeforeMount } from "vue";
import PureHome from "@/views/pure/PureHome.vue";
import { usePostsStore } from "@/store/local";
import { getFeed } from "@/services/api-interface";

export default {
  components: { PureHome },
  setup() {
    const { posts, setValue } = usePostsStore();

    onBeforeMount(async () => {
      const response = await getFeed();
      setValue(response);
    });

    return {
      posts
    };
  }
};
</script>

