<template>
  <div v-if="profile">
    <div class="p-2 bg-purple-100">
      <p>
        {{ profile.account.username }}
      </p>
      <p>Followers: {{ profile.followers }} </p>
      <p>Following: {{ profile.following }}</p>
      <p>Total posts: {{ profile.totalPosts }}</p>

      <button class="btn btn-primary">
        Follow
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import ProfileModel from "@/models/ProfileModel";

export default {
  props: {
    profile: {
      type: ProfileModel,
      required: true
    }
  }
};
</script>

