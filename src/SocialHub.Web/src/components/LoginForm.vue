<template>
  <form>
    <fieldset>
      <!-- Password -->
      <label for="email">Email</label>
      <input
        id="email"
        type="text"
        placeholder="Email"
      >

      <!-- Username -->
      <label for="username">Username: {{ username }}</label>
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
        type="password"
        placeholder="Password"
      >
    </fieldset>
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

    return {
      ...toRefs(formModel)
    };
  }
};
</script>

