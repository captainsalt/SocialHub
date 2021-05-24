<template>
  <PureProfile :account="account" :posts="posts"/>
</template>

<script lang="ts">
import { onBeforeMount } from "vue";
import router from "@/router";
import PureProfile from "@/views/pure/PureProfile.vue";
import { usePostsStore } from "@/store/local";
import { getUserFeed } from "@/services/api-interface";
import AccountModel from "@/models/AccountModel";

export default {
  components: { PureProfile },
  setup() {
    const { posts, setValue } = usePostsStore();

    onBeforeMount(async () => {
      const username = router.currentRoute.value.params.username;
      const response = await getUserFeed(username.toString());
      setValue(response);
    });

    return {
      posts,
      account: new AccountModel("MOCK USER", "MOCK USER", "MOCK USER", new Date())
    };
  }
};
</script>
