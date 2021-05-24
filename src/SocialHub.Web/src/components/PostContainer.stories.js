import PostContainer from "./PostContainer";
import * as PostStories from "./Post.stories";

export default {
  title: "Components/PostContainer",
  component: PostContainer
};

const posts = [];

for (let i = 0; i < 20; i++)
  posts.push(PostStories.Default.args.post);

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

export const Single = Template.bind({});
Single.args = {
  posts: [
    PostStories.Default.args.post
  ]
};

export const Many = Template.bind({});
Many.args = {
  posts
};
