import PureHome from "./PureHome";
import * as PostContainerStories from "@/components/PostContainer.stories.js";

export default {
  title: "Views/PureHome",
  component: PureHome
};

const Template = args => ({
  components: { PureHome },
  setup: () => ({ args }),
  template: "<PureHome v-bind='args'/>"
});

export const SinglePost = Template.bind({});
SinglePost.args = {
  posts: PostContainerStories.Single.args.posts
};

export const ManyPosts = Template.bind({});
ManyPosts.args = {
  posts: PostContainerStories.Many.args.posts
};
