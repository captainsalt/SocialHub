import BottomNav from "./BottomNav";

export default {
  title: "Components/BottomNav",
  components: { BottomNav }
};

const Template = args => ({
  components: { BottomNav },
  setup: () => ({ args }),
  template: "<BottomNav v-bind='args'/>"
});

export const Default = Template.bind({});
Default.args = {
};

