import AccountModel from "@/models/AccountModel";
import PostModel from "@/models/PostModel";
import LoginFormModel from "@/models/LoginFormModel";
import RegisterFormModel from "@/models/RegisterFormModel";
import { token } from "@/store";

interface AuthResponse {
  token: string;
  account: AccountModel;
}

async function fetchRequest<T>(method: string, route: string, options: RequestInit): Promise<T> { // eslint-disable-line no-undef
  const response = await fetch(`${process.env.VUE_APP_API_URL}${route}`, {
    ...options,
    method,
    mode: "cors"
  });

  if (!response.ok)
    throw new Error((await response.json()).message);

  return response.json();
}

export async function login(model: LoginFormModel): Promise<AuthResponse> {
  const options = {
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  };

  const response = await fetchRequest<AuthResponse>("POST", "/api/account/login", options);
  return response;
}

export async function register(model: RegisterFormModel): Promise<AuthResponse> {
  const options = {
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  };

  const response = await fetchRequest<AuthResponse>("POST", "/api/account/create", options);
  return response;
}

export async function getFeed() {
  const response = await fetchRequest<PostModel[]>("GET", "/api/post/feed", {
    headers: {
      "Content-Type": "application/json",
      "Authorization": token.value
    }
  });

  return response;
}

export async function getUserFeed(username: string) {
  const response = await fetchRequest<PostModel[]>("GET", `/api/post/feed/${username}`, {
    headers: {
      "Content-Type": "application/json",
      "Authorization": token.value
    }
  });

  return response;
}

export async function createPost(content: string) {
  const options = {
    body: JSON.stringify({
      content
    }),
    headers: {
      "Content-Type": "application/json",
      "Authorization": token.value
    }
  };

  const response = await fetchRequest<PostModel[]>("POST", `/api/post/create`, options);

  return response;
}
