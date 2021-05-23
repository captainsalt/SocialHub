import PostContainer from "./PostContainer";
import * as PostStories from "./Post.stories";

export default {
  title: "Components/PostContainer",
  component: PostContainer
};

const Template = args => ({
  components: { PostContainer },
  setup() {
    return {
      args
    };
  },
  template: "<PostContainer v-bind='args' />"
});

export const Empty = Template.bind({});
Empty.args = {
};

export const Content = Template.bind({});
Content.args = {
  posts: [
    PostStories.Default.args.post,
    PostStories.Default.args.post,
    PostStories.Default.args.post
  ]
};
