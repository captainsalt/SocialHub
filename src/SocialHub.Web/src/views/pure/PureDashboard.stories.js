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

export const Default = Template.bind({});
Default.args = {
  posts: PostContainerStories.Content.args.posts
};
