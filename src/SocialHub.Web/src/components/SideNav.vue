<template>
  <div class="flex flex-col">
    <a class="nav-item group" @click="$router.push('/home')">
      <HomeIcon class="nav-image"/>
      <span class="nav-label">Home</span>
    </a>

    <a class="nav-item group" @click="$router.push(`/${account.username}`)">
      <UserIcon class="nav-image "/>
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
  @apply p-5 flex text-center items-center justify-items-center h-auto;
}

.nav-item:hover {
  @apply bg-purple-500 cursor-pointer;
}

.nav-item:active {
  @apply bg-purple-600 outline-none;
}

.nav-item:focus {
  @apply outline-none;
}

.nav-image {
  @apply h-7 group-hover:text-white;
}

.nav-label {
  @apply ml-4 text-xl group-hover:text-white;
}
</style>
