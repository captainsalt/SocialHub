<template>
  <div class="flex flex-col">
    <a class="nav-item" @click="$router.push('/home')">
      <HomeIcon class="nav-image"/>
      <span class="nav-label">Home</span>
    </a>

    <a class="nav-item" @click="$router.push(`/${account.username}`)">
      <UserIcon class="nav-image"/>
      <span class="nav-label">Profile</span>
    </a>
  </div>
</template>

<script lang="ts">
import { HomeIcon, UserIcon } from "@heroicons/vue/outline";
import { account } from "@/store";

export default {
  components: {
    HomeIcon,
    UserIcon
  },
  setup() {
    return {
      account
    };
  }
};
</script>

<style scoped>
.nav-item {
  @apply p-5 flex justify-center text-center rounded-full mx-5;
}

.nav-item:hover {
  @apply bg-purple-100 cursor-pointer;
}

.nav-item:active {
  @apply bg-purple-400 outline-none;
}

.nav-item:active * {
  @apply text-white;
}

.nav-item:focus {
  @apply outline-none;
}

.nav-image {
  @apply h-7;
}

.nav-label {
  @apply ml-4 hidden lg:block text-xl;
}
</style>
