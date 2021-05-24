<template>
  <div>
    <div class="p-2 bg-purple-100">
      <p>
        {{ account.username }}
      </p>
      <p>Followers: 0</p>
      <p>Following: 0</p>

      <button class="btn btn-primary">
        Follow
      </button>
    </div>

    <PostContainer :posts="posts"/>
  </div>
</template>

<script lang="ts">
import AccountModel from "@/models/AccountModel";
import PostContainer from "@/components/PostContainer.vue";

export default {
  components: { PostContainer },
  props: {
    account: {
      type: AccountModel,
      required: true
    },
    posts: {
      type: Array,
      required: true
    }
  }
};
</script>
