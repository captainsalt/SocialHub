import Post from "./Post.vue";
import Account from "@/models/Account";

export default {
  title: "Components/Post",
  component: Post
};

const author = new Account("test@test.com", "GenericUser");

const Template = args => ({
  components: { Post },
  setup() {
    args.author = author;

    return {
      args
    };
  },
  template: "<Post v-bind='args' />"
});

export const Plain = Template.bind({})