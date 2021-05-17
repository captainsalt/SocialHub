import Account from "@/models/Account";
import RegisterFormModel from "@/models/RegisterFormModel";

// TODO: Make an API calling service
async function register(model: RegisterFormModel): Promise<{ token: string, account: Account}> {
  const result = await fetch(`${process.env.VUE_APP_API_URL}/api/account/create`, {
    method: "POST",
    body: JSON.stringify(model),
    headers: {
      "Content-Type": "application/json"
    }
  });

  if (result.status < 200 || result.status > 299)
    throw new Error("Error registering");

  // eslint-disable-next-line no-undef
  const response = await result.json();
  return response;
}

export {
  register
};
