import { Auth0Client } from "@auth0/auth0-spa-js";

const auth0 = new Auth0Client({
  domain: process.env.VUE_APP_DOMAIN,
  client_id: process.env.VUE_APP_CLIENT_ID,
  redirect_uri: process.env.VUE_APP_REDIRECT,
  cacheLocation: "localstorage"
});

export default auth0;
