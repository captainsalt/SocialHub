<template>
  <form>
    <div class="form">
      <!-- Password -->
      <label class="label">Email</label>
      <input
        v-model="email"
        class="input"
        type="text"
        required
      >

      <!-- Username -->
      <label class="mt-2 label" for="username">Username</label>
      <input
        id="username"
        v-model="username"
        class="input"
        type="text"
        required
      >

      <!-- Password -->
      <label class="mt-2 label" for="password">Password</label>
      <input
        id="password"
        v-model="password"
        class="input"
        type="password"
        required
      >

      <button class="w-full mt-2 btn btn-primary" @click.prevent="registerUser">
        Register
      </button>
    </div>
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

