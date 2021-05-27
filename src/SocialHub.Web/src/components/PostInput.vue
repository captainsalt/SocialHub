<template>
  <div class="grid grid-cols-12 gap-1 auto-rows-auto">
    <Alert
      v-if="errorMessage"
      type="error"
      class="row-start-1 col-span-full"
      :message="errorMessage"
      @vnode-mounted="$emit('error')"
    />

    <input
      v-model="input"
      class="col-span-9 row-start-2 input"
      placeholder="Send a post!"
      @keydown.enter="sendPost"
    >

    <button class="col-span-3 row-start-2 btn btn-primary" @click="sendPost">
      Send
    </button>
  </div>
</template>

<script lang="ts">
import { ref } from "vue";
import { createPost } from "@/services/api-interface";
import Alert from "@/components/Alert.vue";
const errorMessage = ref("");

export default {
  components: { Alert },
  emits: {
    error: null
  },
  setup() {
    const input = ref("");

    async function sendPost() {
      try {
        await createPost(input.value);
        input.value = "";
      }
      catch (response) {
        errorMessage.value = response.message ?? "Error sending post";
      }
    }

    return {
      input,
      sendPost,
      errorMessage
    };
  }
};
</script>
