<template>
  <div>
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

    <PostContainer :posts="posts"/>
  </div>
</template>

<script lang="ts">
import ProfileModel from "@/models/ProfileModel";
import PostContainer from "@/components/PostContainer.vue";

export default {
  components: { PostContainer },
  props: {
    profile: {
      type: ProfileModel,
      required: true
    },
    posts: {
      type: Array,
      required: true
    }
  }
};
</script>
