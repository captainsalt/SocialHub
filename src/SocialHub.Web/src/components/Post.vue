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
        v-if="isLiked"
        class="text-red-500 post-button hover:opacity-50"
        @click="removePostLike"
      />
      <HeartOutline
        v-else
        class="hover:text-red-500 post-button hover:stroke-current"
        @click="likePost"
      />

      <RefreshSolid
        v-if="isShared"
        class="text-green-500 post-button hover:opacity-50"
        @click="removePostShare"
      />
      <RefreshOutline
        v-else
        class="hover:text-green-500 post-button hover:stroke-current"
        @click="sharePost"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { ref } from "vue";
import PostModel from "@/models/PostModel";
import { share, like, removeLike, removeShare } from "@/services/api-interface";
import {
  HeartIcon as HeartOutline,
  RefreshIcon as RefreshOutline
} from "@heroicons/vue/outline";
import {
  HeartIcon as HeartSolid,
  RefreshIcon as RefreshSolid
} from "@heroicons/vue/solid";

interface Props {
  post: PostModel
}

export default {
  components: {
    HeartOutline,
    RefreshOutline,
    HeartSolid,
    RefreshSolid
  },
  props: {
    post: {
      type: PostModel,
      required: true
    }
  },
  setup(props: Props) {
    const isLiked = ref(props.post.isLiked);
    const isShared = ref(props.post.isShared);
    const postId = ref(props.post.id);

    async function likePost() {
      await like(postId.value);
      isLiked.value = true;
    }

    async function sharePost() {
      await share(postId.value);
      isShared.value = true;
    }

    async function removePostLike() {
      await removeLike(postId.value);
      isLiked.value = false;
    }

    async function removePostShare() {
      await removeShare(postId.value);
      isShared.value = false;
    }

    return {
      isLiked,
      isShared,
      likePost,
      sharePost,
      removePostLike,
      removePostShare
    };
  }
};
</script>

<style scoped>
.post-button {
  @apply h-5;
}

.post-button:hover {
  @apply cursor-pointer;
}
</style>
