<template>
  <div class="grid grid-cols-12 gap-1 auto-rows-auto">
    <Alert
      v-if="errorMessage"
      type="error"
      class="row-start-1 col-span-full"
      :message="errorMessage"
    />

    <input
      v-model="input"
      class="col-span-9 row-start-2 input"
      placeholder="Send a post!"
      @keydown.enter="sendPost"
    >

    <Button
      :is-loading="isLoading"
      class="col-span-3 row-start-2 btn btn-primary"
      @click="sendPost"
    >
      Send
    </Button>
  </div>
</template>

<script lang="ts">
import { ref } from "vue";
import { createPost } from "@/services/api-interface";
import Alert from "@/components/Alert.vue";
const errorMessage = ref("");

export default {
  components: { Alert },
  setup() {
    const input = ref("");
    const isLoading = ref(false);

    async function sendPost() {
      try {
        isLoading.value = true;
        await createPost(input.value);
        input.value = "";
      }
      catch (response) {
        errorMessage.value = response.message ?? "Error sending post";
      }
      finally {
        isLoading.value = false;
      }
    }

    return {
      input,
      sendPost,
      errorMessage,
      isLoading
    };
  }
};
</script>
