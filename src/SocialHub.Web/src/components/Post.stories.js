import Post from "./Post";
import PostModel from "@/models/PostModel";
import AccountModel from "@/models/AccountModel";

export default {
  title: "Components/Post",
  component: PostModel
};

const author = new AccountModel("test@test.com", "GenericUser");
const post = new PostModel(author, "Hello world");

const Template = args => ({
  components: { Post },
  setup() {
    args.author = author;
    args.post = post;

    return {
      args
    };
  },
  template: "<Post v-bind='args' />"
});

export const Plain = Template.bind({});
Plain.args = {

};
