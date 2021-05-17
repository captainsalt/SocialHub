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

    <button @click.prevent="registerUser">
      Register
    </button>
  </form>
</template>

<script lang="ts">
import RegisterFormModel from "@/models/RegisterFormModel";
import { reactive, toRefs } from "vue";
import { register } from "@/services/api-interface";

export default {
  setup() {
    const formModel = reactive(new RegisterFormModel("", "", ""));

    async function registerUser() {
      const token = await register(formModel);
    }

    return {
      ...toRefs(formModel),
      registerUser
    };
  }
};
</script>

