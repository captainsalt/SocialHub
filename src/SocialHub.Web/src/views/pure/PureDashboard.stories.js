import PureDashboard from "./PureDashboard";
import * as PostContainerStories from "@/components/PostContainer.stories.js";

export default {
  title: "Views/PureDashboard",
  component: PureDashboard
};

const Template = args => ({
  components: { PureDashboard },
  setup: () => ({ args }),
  template: "<PureDashboard v-bind='args'/>"
});

export const SinglePost = Template.bind({});
SinglePost.args = {
  posts: PostContainerStories.Single.args.posts
};

export const ManyPosts = Template.bind({});
ManyPosts.args = {
  posts: PostContainerStories.Many.args.posts
};
