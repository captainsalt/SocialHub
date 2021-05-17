import { computed, reactive } from "vue";

const state = reactive({
  token: localStorage.getItem("token") || ""
});

const getToken = computed(() => state.token);
const setToken = (token: string) => {
  localStorage.setItem("token", token);
  state.token = token;
};

export {
  getToken,
  setToken
};
