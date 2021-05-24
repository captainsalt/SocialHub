<template>
  <div class="grid items-center w-screen h-screen p-5 lg:p-96">
    <div>
      <LoginForm v-if="showLogin"/>
      <RegisterForm v-else/>

      <a
        class="block mt-5 text-xl text-center lg:text-base anchor"
        @click="showLogin = !showLogin"
      >
        {{ showLogin ? "Register here" : "Log in here" }}
      </a>
    </div>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import RegisterForm from "@/components/RegisterForm.vue";
import LoginForm from "@/components/LoginForm.vue";
import { ref } from "vue";

const showLogin = ref(true);
</script>
