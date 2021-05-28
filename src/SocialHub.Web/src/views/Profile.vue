<template>
  <Alert
    v-if="notFound"
    type="error"
    message="User not found"
    class="w-11/12 mx-auto mt-2"
  />
  <PureProfile
    v-else
    :profile="profile"
    :posts="posts"
  />
</template>

<script lang="ts">
import { onBeforeMount, ref, watch } from "vue";
import router from "@/router";
import PureProfile from "@/views/pure/PureProfile.vue";
import { usePostsStore } from "@/store/local";
import { getUserFeed, getProfile } from "@/services/api-interface";
import ProfileModel from "@/models/ProfileModel";
import Alert from "@/components/Alert.vue";

export default {
  components: {
    PureProfile,
    Alert
  },
  setup() {
    const { posts, setValue: setPostsValue } = usePostsStore();
    const profile = ref<ProfileModel>();
    const notFound = ref(false);

    async function displayProfile() {
      const username = router.currentRoute.value.params.username?.toString();
      if (!username)
        return;

      try {
        const profileResponse = await getProfile(username);
        const feedResponse = await getUserFeed(username);
        setPostsValue(feedResponse);
        profile.value = profileResponse;
      }
      catch (err) {
        if (err.status === 400)
          notFound.value = true;
      }
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
      profile,
      notFound
    };
  }
};
</script>
