<template>
  <form>
    <div>
      <Alert
        v-if="errorMessage"
        type="error"
        class="mb-2"
        :message="errorMessage"
      />

      <label class="label">Username</label>
      <input
        v-model="username"
        class="input"
        type="text"
        required
      >

      <label class="mt-2 label">Password</label>
      <input
        v-model="password"
        class="input"
        type="password"
        required
      >

      <button class="w-full mt-2 btn btn-primary" @click.prevent="userLogin">
        Log in
      </button>
    </div>
  </form>
</template>

<script lang="ts">
import LoginFormModel from "@/models/LoginFormModel";
import Alert from "@/components/Alert.vue";
import { reactive, toRefs, ref } from "vue";
import { login } from "@/services/api-interface";
import { setAccount, setToken } from "@/store";
import router from "@/router";

export default {
  components: { Alert },
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
        errorMessage.value = error.message ?? "Error";
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

