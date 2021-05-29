import SideNav from "./SideNav";

export default {
  title: "Components/SideNav",
  component: SideNav
};

const Template = args => ({
  components: { SideNav },
  setup: () => ({ args }),
  template: "<SideNav v-bind='args'/>"
});

export const Default = Template.bind({});
Default.args = {

};
