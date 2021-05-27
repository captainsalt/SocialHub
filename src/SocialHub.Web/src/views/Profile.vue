<template>
  <PureProfile :profile="profile" :posts="posts"/>
</template>

<script lang="ts">
import { onBeforeMount, ref, watch } from "vue";
import router from "@/router";
import PureProfile from "@/views/pure/PureProfile.vue";
import { usePostsStore } from "@/store/local";
import { getUserFeed, getProfile } from "@/services/api-interface";
import ProfileModel from "@/models/ProfileModel";

export default {
  components: { PureProfile },
  setup() {
    const { posts, setValue: setPostsValue } = usePostsStore();
    const profile = ref<ProfileModel>();

    async function displayProfile() {
      const username = router.currentRoute.value.params.username?.toString();
      if (!username)
        return;

      const profileResponse = await getProfile(username);
      const feedResponse = await getUserFeed(username);

      setPostsValue(feedResponse);
      profile.value = profileResponse;
    }

    onBeforeMount(async () => {
      await displayProfile();
    });

    watch(router.currentRoute, (newRoute, oldRoute) => {
      if (newRoute.params.username !== oldRoute.params.username)
        displayProfile();
    });

    return {
      posts,
      profile
    };
  }
};
</script>
