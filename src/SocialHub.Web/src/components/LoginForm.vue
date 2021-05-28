<template>
  <form>
    <div>
      <div v-if="errorMessages.length > 0">
        <Alert
          v-for="(msg, i) in errorMessages"
          :key="i"
          class="mb-2 text-center"
          type="error"
          :message="msg"
        />
      </div>

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

      <Button
        :is-loading="isLoading"
        class="w-full mt-2 btn-primary"
        @click.prevent="userLogin"
      >
        Log in
      </Button>
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
    const errorMessages = ref<string[]>([]);
    const formModel = reactive(new LoginFormModel());
    const isLoading = ref(false);

    function validate() {
      errorMessages.value = [];
      const errors = [];
      const usernameRegex = /^[\w_-]{3,}$/;
      const passwordRegex = /^.{3,}$/;

      if (!usernameRegex.test(formModel.username))
        errors.push("Username must be at least 3 characters long");

      if (!passwordRegex.test(formModel.password))
        errors.push("Password must be at least 3 characters long");

      errorMessages.value = errors;
    }

    async function userLogin() {
      try {
        isLoading.value = true;
        errorMessages.value = [];

        validate();

        if (errorMessages.value.length > 0)
          return;

        const response = await login(formModel);
        setToken(response.token);
        setAccount(response.account);

        await router.push("/home");
      }
      catch (error) {
        errorMessages.value.push(error.message ?? "Error");
        isLoading.value = false;
      }
    }

    return {
      ...toRefs(formModel),
      errorMessages,
      userLogin,
      isLoading
    };
  }
};
</script>

