import PureProfile from "./PureProfile";
import * as PostContainerStories from "@/components/PostContainer.stories.js";
import * as ProfileHeaderStories from "@/components/ProfileHeader.stories.js";

const profile = ProfileHeaderStories.Default.args.profile;

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
  profile,
  posts: PostContainerStories.Single.args.posts
};

export const ManyPosts = Template.bind({});
ManyPosts.args = {
  profile,
  posts: PostContainerStories.Many.args.posts
};
