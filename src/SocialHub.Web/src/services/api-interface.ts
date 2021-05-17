import { Account } from "@/models/Account";
import LoginFormModel from "@/models/LoginFormModel";
import RegisterFormModel from "@/models/RegisterFormModel";

interface AuthResponse {
  token: string;
  account: Account;
}

async function fetchRequest<T>(method: string, route: string, options: RequestInit): Promise<T> { // eslint-disable-line no-undef
  const response = await fetch(`${process.env.VUE_APP_API_URL}${route}`, {
    ...options,
    method,
    mode: "cors"
  });

  if (!response.ok)
    throw new Error(await response.text());

  return response.json();
}

async function login(model: LoginFormModel): Promise<AuthResponse> {
  const options = {
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  };

  const response = await fetchRequest<AuthResponse>("POST", "/api/account/login", options);
  return response;
}

async function register(model: RegisterFormModel): Promise<AuthResponse> {
  const options = {
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  };

  const response = await fetchRequest<AuthResponse>("POST", "/api/account/create", options);
  return response;
}

export {
  register,
  login
};
