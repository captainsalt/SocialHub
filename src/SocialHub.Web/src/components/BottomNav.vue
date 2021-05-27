<template>
  <div>
    <div class="flex justify-around w-full bg-purple-500">
      <!-- Home -->
      <div
        class="nav-button"
        @click="$emit('onNav', '/home')"
      >
        <HomeIcon class="group-hover:text-white"/>
        <span class="group-hover:text-white">
          Home
        </span>
      </div>

      <!-- Profile -->
      <div
        class="nav-button"
        @click="$emit('onNav', `/${account.username}`)"
      >
        <UserIcon class="group-hover:text-white"/>
        <span class="group-hover:text-white">
          Profile
        </span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { account } from "@/store";
import { HomeIcon, UserIcon } from "@heroicons/vue/outline";

export default {
  components: {
    HomeIcon,
    UserIcon
  },
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
