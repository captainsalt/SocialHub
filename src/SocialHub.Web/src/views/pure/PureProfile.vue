<template>
  <div>
    <ProfileHeader :profile="profile"/>
    <PostContainer :posts="posts"/>
  </div>
</template>

<script lang="ts">
import ProfileModel from "@/models/ProfileModel";
import PostContainer from "@/components/PostContainer.vue";
import ProfileHeader from "@/components/ProfileHeader.vue";

export default {
  components: { PostContainer, ProfileHeader },
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
