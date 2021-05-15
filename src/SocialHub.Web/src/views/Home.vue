<template>
  <div>
    <LoginForm/>
  </div>
</template>

<script lang="ts" setup>
/* eslint-disable no-unused-vars, @typescript-eslint/no-unused-vars */
import auth0 from "@/services/auth0";
import router from "@/router";
import { onMounted } from "@vue/runtime-core";
import LoginForm from "@/components/LoginForm.vue";

onMounted(async () => {
  if (window.location.search.includes("code=") &&
    window.location.search.includes("state=")) {
    await auth0.handleRedirectCallback();

    router.push("/dashboard");
  }
});

async function login() {
  await auth0.loginWithRedirect();
}

async function logout() {
  await auth0.logout();
}
</script>
