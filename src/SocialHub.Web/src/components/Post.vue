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
        class="text-red-500 svg"
        @click="removePostLike"
      />
      <HeartOutline
        v-else
        class="svg"
        @click="likePost"
      />

      <SpeakerSolid
        v-if="isShared"
        class="text-green-500 svg"
        @click="removePostShare"
      />
      <SpeakerOutline
        v-else
        class="svg"
        @click="sharePost"
      />
    </div>
  </div>
</template>

<script lang="ts">
import PostModel from "@/models/PostModel";
import { share, like, removeLike, removeShare } from "@/services/api-interface";
import { HeartIcon as HeartOutline, SpeakerphoneIcon as SpeakerOutline } from "@heroicons/vue/outline";
import { HeartIcon as HeartSolid, SpeakerphoneIcon as SpeakerSolid } from "@heroicons/vue/solid";
import { ref } from "@vue/reactivity";

interface Props {
  post: PostModel
}

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
.svg {
  @apply h-5;
}

.svg:hover {
  @apply cursor-pointer;
}
</style>
