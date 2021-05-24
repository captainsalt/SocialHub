<template>
  <div class="w-full text-white rounded-md" :class="alertClasses">
    <div class="container flex items-center justify-between px-6 py-4 mx-auto">
      <div class="mx-auto">
        {{ message }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { reactive } from "vue";

interface Props {
  type: string,
  message: string
}

export default {
  props: {
    type: {
      type: String,
      required: true,
      validator: (val: string) =>
        ["warning", "error", "info", "success"].indexOf(val) !== -1
    },
    message: {
      type: String,
      required: true
    }
  },
  setup(props: Props) {
    const alertClasses = reactive({
      "bg-red-500": props.type === "error",
      "bg-yellow-400": props.type === "warning",
      "bg-blue-500": props.type === "info",
      "bg-green-500": props.type === "success"
    });

    return {
      alertClasses
    };
  }
};
</script>
