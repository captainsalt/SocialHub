<template>
  <div>
    <div>
      Username: {{ author.username }}
    </div>
  </div>
</template>

<script lang="ts">
import Account from "@/models/Account";

export default {
  props: {
    author: {
      type: Account,
      required: true
    }
  }
};
</script>
