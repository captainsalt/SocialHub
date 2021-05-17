import { Account } from "@/models/Account";
import { computed, reactive } from "vue";

const state = reactive({
  token: localStorage.getItem("token") ?? "",
  account: localStorage.getItem("account") ?? ""
});

const token = computed(() => state.token);
const setToken = (token: string) => {
  localStorage.setItem("token", token);
  state.token = token;
};

const account = computed<Account>(() => JSON.parse(state.account));
const setAccount = (account: Account) => {
  const stringAccount = JSON.stringify(account);
  localStorage.setItem("account", stringAccount);
  state.account = stringAccount;
};

export {
  token,
  setToken,
  account,
  setAccount
};
