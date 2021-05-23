<template>
  <div class="w-full p-3 border-2 border-purple-300">
    <!-- Header -->
    <div class="">
      {{ author.username }} <span class="text-sm text-gray-600">{{ post.createdAt }}</span>
    </div>

    <!-- Content -->
    <div class="mt-2 break-words">
      {{ post.content }}
    </div>

    <!-- Footer -->
    <div class="flex justify-around text-gray-600" @click="$emit('postShared', post.id)">
      <div class="hover:underline hover:cursor-pointer">
        Shares: 0
      </div>

      <div class="hover:underline hover:cursor-pointer" @click="$emit('postLiked', post.id)">
        Likes: 0
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import AccountModel from "@/models/AccountModel";
import PostModel from "@/models/PostModel";

export default {
  props: {
    author: {
      type: AccountModel,
      required: true
    },
    post: {
      type: PostModel,
      required: true
    }
  },
  emits: {
    postLiked: null,
    postShared: null
  }
};
</script>
