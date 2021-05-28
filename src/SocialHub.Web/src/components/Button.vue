<template>
  <button class="btn" :disabled="isLoading">
    <div v-if="isLoading" class="flex justify-center">
      <Spinner/>
    </div>
    <div v-else>
      <slot/>
    </div>
  </button>
</template>

<script lang="ts">
import Spinner from "@/components/Spinner.vue";

export default {
  components: { Spinner },
  props: {
    isLoading: Boolean
  }
};
</script>
