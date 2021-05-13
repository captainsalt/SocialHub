<template>
  <div>
    <label for="email"> {{ email }}</label>
    <input
      v-model="email"
      placeholder="email"
      type="text"
    >
    <label for=""> {{ password }}</label>
    <input v-model="password" type="password">
    <button @click="send">
      Send
    </button>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";

const email = ref("");
const password = ref("");

function send() {
  email.value = "send";
  password.value = "yosha";
}
</script>
