import Post from "./Post";
import PostModel from "@/models/PostModel";
import AccountModel from "@/models/AccountModel";

export default {
  title: "Components/Post",
  component: PostModel
};

const author = new AccountModel("111-111-111-111", "test@test.com", "GenericUser", new Date());
const post = new PostModel("111-111-111-111", author, "Hello world", new Date());

const Template = args => ({
  components: { Post },
  setup() {
    return {
      args
    };
  },
  template: "<Post v-bind='args' />"
});

export const Default = Template.bind({});
Default.args = {
  post
};
