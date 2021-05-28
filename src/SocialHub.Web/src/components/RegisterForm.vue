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

      <!-- Password -->
      <label class="label">Email</label>
      <input
        id="email"
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

      <Button
        :is-loading="isLoading"
        class="w-full mt-2 btn btn-primary"
        @click.prevent="registerUser"
      >
        Register
      </Button>
    </div>
  </form>
</template>

<script lang="ts">
import { reactive, toRefs, ref } from "vue";
import { register } from "@/services/api-interface";
import { setAccount, setToken } from "@/store";
import router from "@/router";
import RegisterFormModel from "@/models/RegisterFormModel";
import Alert from "@/components/Alert.vue";

export default {
  components: { Alert },
  setup() {
    const errorMessages = ref<string[]>([]);
    const formModel = reactive(new RegisterFormModel());
    const isLoading = ref(false);

    function validate() {
      errorMessages.value = [];
      const errors = [];
      const emailRegex = /^.+?@.+\.(fake)$/;
      const usernameRegex = /^[\w_-]{3,}$/;
      const passwordRegex = /^.{3,}$/;

      if (!emailRegex.test(formModel.email))
        errors.push("Invalid email. Emails must end with the 'fake' domain - debbie@domain.fake");

      if (!usernameRegex.test(formModel.username))
        errors.push("Username must be at least 3 characters long");

      if (!passwordRegex.test(formModel.password))
        errors.push("Password must be at least 3 characters long");

      errorMessages.value = errors;
    }

    async function registerUser() {
      try {
        isLoading.value = true;

        validate();

        if (errorMessages.value.length > 0)
          return;

        const response = await register(formModel);
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
      registerUser,
      isLoading
    };
  }
};
</script>

