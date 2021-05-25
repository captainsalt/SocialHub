<template>
  <div>
    <div class="flex justify-around w-full bg-purple-500">
      <!-- Home -->
      <div
        class="nav-button"
        @click="$emit('onNav', '/home')"
      >
        <svg
          class="group-hover:text-white"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6"
          />
        </svg>

        <span class="group-hover:text-white">
          Home
        </span>
      </div>

      <!-- Profile -->
      <div
        class="nav-button"
        @click="$emit('onNav', `/profile/${account.username}`)"
      >
        <svg
          class="group-hover:text-white"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M16 7a4 4 0 11-8 0 4 4 0 018 0zm-4 7a7 7 0 00-7 7h14a7 7 0 00-7-7z"
          />
        </svg>

        <span class="group-hover:text-white">
          Profile
        </span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { account } from "@/store";

export default {
  emits: {
    onNav: null
  },
  setup() {
    return {
      account
    };
  }
};
</script>

<style scoped>
.nav-button {
  @apply flex flex-col flex-wrap items-center justify-center p-2 w-52 group hover:cursor-pointer hover:bg-purple-600;
}

.nav-button:active {
  @apply bg-purple-700;
}

.nav-button svg {
  @apply h-7;
}
</style>
