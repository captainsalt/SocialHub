<template>
  <div>
    <div>This is the dashboard</div>
    <div>
      Current User: {{ user?.name }}
    </div>

    <button @click="callApi">
      Call Api
    </button>
    <p> {{ result }} </p>
  </div>
</template>

<script lang="ts" setup>
import auth0 from "@/services/auth0";
import { User } from "@auth0/auth0-spa-js";
import { onBeforeMount, ref } from "vue";

const user: User = ref(undefined);
const result = ref("");

onBeforeMount(async () => {
  user.value = await auth0.getUser();
});

async function callApi() { // eslint-disable-line
  const token = await auth0.getTokenSilently();

  const response = await fetch(`${process.env.VUE_APP_API_URL}/weatherforecast`, {
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    }
  });

  result.value = await response.text();
}
</script>
