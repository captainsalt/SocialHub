<template>
  <div v-if="profile">
    <div class="p-2 bg-purple-100">
      <p>
        {{ profile.account.username }}
      </p>
      <p>Followers: {{ profile.followers }} </p>
      <p>Following: {{ profile.following }}</p>
      <p>Total posts: {{ profile.totalPosts }}</p>

      <button
        v-if="isFollowing"
        class="btn btn-primary"
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
</template>

<script lang="ts">
import ProfileModel from "@/models/ProfileModel";
import { follow, unfollow } from "@/services/api-interface";
import { ref } from "vue";

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

    const interval = setInterval(() => {
      if (props.profile !== null) {
        isFollowing.value = props.profile?.isFollowing;
        clearInterval(interval);
      }
    }, 300);

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
      isFollowing
    };
  }
};
</script>
