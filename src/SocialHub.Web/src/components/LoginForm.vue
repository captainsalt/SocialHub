<template>
  <form>
    <fieldset>
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
      <button @click.prevent="userLogin">
        Log in
      </button>
    </fieldset>
  </form>
</template>

<script lang="ts">
import LoginFormModel from "@/models/LoginFormModel";
import { reactive, toRefs, ref } from "vue";
import { login } from "@/services/api-interface";
import { setAccount, setToken } from "@/store";
import router from "@/router";

export default {
  setup() {
    const errorMessage = ref("");
    const formModel = reactive(new LoginFormModel());

    async function userLogin() {
      try {
        const response = await login(formModel);
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
      userLogin
    };
  }
};
</script>

