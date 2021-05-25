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
    const profile = ref<ProfileModel | null>(null);

    async function setProfile() {
      const username = router.currentRoute.value.params.username.toString();
      const feedResponse = await getUserFeed(username);
      const profileResponse = await getProfile(username);

      setPostsValue(feedResponse);
      profile.value = profileResponse;
    }

    onBeforeMount(async () => {
      await setProfile();
    });

    watch(router.currentRoute, (newRoute, oldRoute) => {
      if (newRoute.params.username !== oldRoute.params.username)
        setProfile();
    });

    return {
      posts,
      profile
    };
  }
};
</script>
