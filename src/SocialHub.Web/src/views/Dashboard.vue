<template>
  <div>
    <div>This is the dashboard</div>
    <div>
      Current User: {{ user?.name }}
    </div>
  </div>
</template>

<script lang="ts" setup>
import auth0 from "@/services/auth0";
import { User } from "@auth0/auth0-spa-js";
import { ref } from "vue";

const user: User = ref(undefined);

(async () => {
  user.value = await auth0.getUser();
})();
</script>
