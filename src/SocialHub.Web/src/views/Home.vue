<template>
  <div>
    <button @click="login">
      Login
    </button>

    <button @click="logout">
      Logout
    </button>
  </div>
</template>

<script lang="ts" setup>
import auth0 from "@/services/auth0";
import router from "@/router";
import { onMounted } from "@vue/runtime-core";

onMounted(async () => {
  if (window.location.search.includes("code=") &&
    window.location.search.includes("state=")) {
    await auth0.handleRedirectCallback();

    router.push("/dashboard");
  }
});

async function login() { // eslint-disable-line
  await auth0.loginWithRedirect();
}

async function logout() { // eslint-disable-line
  await auth0.logout();
}
</script>
