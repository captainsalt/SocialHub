<template>
  <div class="p-3 border-collapse border-gray-300">
    <!-- Header -->
    <div>
      {{ post.account.username }}
      <span class="text-sm text-gray-600">{{ post.createdAt }}</span>
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
import PostModel from "@/models/PostModel";

export default {
  props: {
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
