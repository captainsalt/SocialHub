import PostInput from "./PostInput.vue";

export default {
  title: "Components/PostInput",
  component: PostInput
};

const Template = args => ({
  components: { PostInput },
  setup: () => ({ args }),
  template: "<PostInput v-bind='args'/>"
});

export const Default = Template.bind({});
Default.args = {
};
