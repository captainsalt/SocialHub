import PureProfile from "./PureProfile";
import * as PostStories from "@/components/Post.stories.js";
import * as PostContainerStories from "@/components/PostContainer.stories.js";

const account = PostStories.Default.args.post.account;

export default {
  title: "Views/Pure/PureProfile",
  component: PureProfile
};

const Template = args => ({
  components: { PureProfile },
  setup: () => ({ args }),
  template: "<PureProfile v-bind='args' />"
});

export const SinglePost = Template.bind({});
SinglePost.args = {
  account,
  posts: [
    PostStories.Default.args.post
  ]
};

export const ManyPosts = Template.bind({});
ManyPosts.args = {
  account,
  posts: PostContainerStories.Many.args.posts
};
