<template>
  <div>
    <RegisterForm v-if="toggle"/>
    <LoginForm v-else/>

    <button @click="toggle = !toggle">
      Toggle
    </button>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import RegisterForm from "@/components/RegisterForm.vue";
import LoginForm from "@/components/LoginForm.vue";
import { ref } from "vue";

const toggle = ref(false);
</script>
