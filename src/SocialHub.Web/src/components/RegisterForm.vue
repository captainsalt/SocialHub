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
      <br>
      <button @click.prevent="registerUser">
        Register
      </button>
    </fieldset>
  </form>
</template>

<script lang="ts">
import RegisterFormModel from "@/models/RegisterFormModel";
import { reactive, toRefs, ref } from "vue";
import { register } from "@/services/api-interface";
import { setAccount, setToken } from "@/store";
import router from "@/router";

export default {
  setup() {
    const errorMessage = ref("");
    const formModel = reactive(new RegisterFormModel());

    async function registerUser() {
      try {
        const response = await register(formModel);
        setToken(response.token);
        setAccount(response.account);

        await router.push("/dashboard");
      }
      catch (error) {
        errorMessage.value = error;
      }
    }

    return {
      ...toRefs(formModel),
      errorMessage,
      registerUser
    };
  }
};
</script>

