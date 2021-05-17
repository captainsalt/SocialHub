import { Account } from "@/models/Account";
import LoginFormModel from "@/models/LoginFormModel";
import RegisterFormModel from "@/models/RegisterFormModel";

interface AuthResponse {
  token: string;
  account: Account;
}

async function register(model: RegisterFormModel): Promise<AuthResponse> {
  const result = await fetch(`${process.env.VUE_APP_API_URL}/api/account/create`, {
    method: "POST",
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  });

  if (!result.ok)
    throw new Error("Error registering");

  const response = await result.json();
  return response;
}

async function login(model: LoginFormModel): Promise<AuthResponse> {
  const result = await fetch(`${process.env.VUE_APP_API_URL}/api/account/login`, {
    method: "POST",
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  });

  if (!result.ok)
    throw new Error("Error logging in");

  const response = await result.json();
  return response;
}

export {
  register,
  login
};
