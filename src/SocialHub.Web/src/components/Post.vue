<template>
  <div>
    <div>
      Username: {{ author.username }}
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
  }
};
</script>
