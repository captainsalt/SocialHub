import AccountModel from "@/models/AccountModel";
import { computed, reactive } from "vue";

export const state = reactive({
  token: localStorage.getItem("token") ?? "",
  account: localStorage.getItem("account") ?? ""
});

export const token = computed(() => state.token);
export const setToken = (token: string) => {
  localStorage.setItem("token", token);
  state.token = token;
};

export const account = computed<AccountModel>(() => JSON.parse(state.account));
export const setAccount = (account: AccountModel) => {
  const stringAccount = JSON.stringify(account);
  localStorage.setItem("account", stringAccount);
  state.account = stringAccount;
};
