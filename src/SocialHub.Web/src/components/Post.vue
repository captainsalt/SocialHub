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
    <div class="flex justify-around">
      <HeartSolid
        v-if="post.isLiked"
        class="text-red-500 svg"
        @click="$emit('likeRemove', post.id)"
      />
      <HeartOutline
        v-else
        class="svg"
        @click="$emit('like', post.id)"
      />

      <SpeakerSolid
        v-if="post.isShared"
        class="text-green-500 svg"
        @click="$emit('shareRemove', post.id) "
      />
      <SpeakerOutline
        v-else
        class="svg"
        @click="$emit('share', post.id) "
      />
    </div>
  </div>
</template>

<script lang="ts">
import PostModel from "@/models/PostModel";
import { HeartIcon as HeartOutline, SpeakerphoneIcon as SpeakerOutline } from "@heroicons/vue/outline";
import { HeartIcon as HeartSolid, SpeakerphoneIcon as SpeakerSolid } from "@heroicons/vue/solid";

export default {
  components: {
    HeartOutline,
    SpeakerOutline,
    HeartSolid,
    SpeakerSolid
  },
  props: {
    post: {
      type: PostModel,
      required: true
    }
  },
  emits: {
    like: null,
    share: null,
    likeRemove: null,
    shareRemove: null
  }
};
</script>

<style scoped>
.svg {
  @apply h-5;
}

.svg:hover {
  @apply cursor-pointer;
}
</style>
