<template>
  <div class="grid grid-cols-12 gap-1">
    <input
      v-model="content"
      class="col-span-9 input"
      placeholder="Send a post!"
      @keydown.enter="sendPost"
    >

    <button class="col-span-3 btn btn-primary" @click="sendPost">
      Send
    </button>
  </div>
</template>

<script lang="ts">
import { ref } from "vue";
import { createPost } from "@/services/api-interface";

export default {
  setup() {
    const content = ref("");

    async function sendPost() {
      await createPost(content.value);
      content.value = "";
    }

    return {
      content,
      sendPost
    };
  }
};
</script>
