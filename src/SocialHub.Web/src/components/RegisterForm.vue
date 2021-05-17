<template>
  <form>
    <fieldset>
      <!-- Password -->
      <label for="email">Email</label>
      <input
        id="email"
        v-model="email"
        type="text"
        placeholder="Email"
      >

      <!-- Username -->
      <label for="username">Username</label>
      <input
        id="username"
        v-model="username"
        type="text"
        placeholder="Username"
      >

      <!-- Password -->
      <label for="password">Password</label>
      <input
        id="password"
        v-model="password"
        type="password"
        placeholder="Password"
      >
    </fieldset>

    <button @click.prevent="register">
      Register
    </button>
  </form>
</template>

<script lang="ts">
import { reactive, toRefs } from "vue";

export default {
  setup() {
    const formModel = reactive({
      username: "",
      password: "",
      email: ""
    });

    function register() {
      fetch(`${process.env.VUE_APP_API_URL}/api/account/create`, {
        method: "POST",
        body: JSON.stringify(formModel),
        headers: [["Content-Type", "application/json"]]
      });
    }

    return {
      ...toRefs(formModel),
      register
    };
  }
};
</script>

