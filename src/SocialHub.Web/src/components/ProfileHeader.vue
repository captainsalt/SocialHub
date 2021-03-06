<template>
  <div v-if="profile">
    <div class="p-2 bg-purple-100 rounded-sm">
      <div class="flex justify-between w-full">
        <!-- Username -->
        <p class="text-3xl">
          {{ profile.account.username }}
        </p>
        <!-- Follow button -->
        <div v-if="showFollowButton">
          <button
            v-if="isFollowing"
            class=" btn btn-primary"
            @click="unfollowUser"
          >
            Unfollow
          </button>
          <button
            v-else
            class="btn btn-primary"
            @click="followUser"
          >
            Follow
          </button>
        </div>
      </div>

      <div class="flex justify-between">
        <p>Followers: {{ profile.followers }}</p>
        <p>Following: {{ profile.following }}</p>
        <p>Total posts: {{ profile.totalPosts }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import ProfileModel from "@/models/ProfileModel";
import { follow, unfollow } from "@/services/api-interface";
import { ref, watch } from "vue";
import { account } from "@/store";

interface Props {
  profile: ProfileModel
}

export default {
  props: {
    profile: {
      type: ProfileModel,
      required: true
    }
  },
  setup(props: Props) {
    const isFollowing = ref(false);
    const showFollowButton = ref(false);

    watch(() => props.profile?.isFollowing, newVal => {
      isFollowing.value = newVal;
    });

    watch(() => props.profile?.account.id, newVal => {
      showFollowButton.value = account.value.id !== newVal;
    });

    async function followUser() {
      await follow(props.profile.account.username);
      isFollowing.value = true;
    }

    async function unfollowUser() {
      await unfollow(props.profile.account.username);
      isFollowing.value = false;
    }

    return {
      followUser,
      unfollowUser,
      isFollowing,
      showFollowButton
    };
  }
};
</script>
