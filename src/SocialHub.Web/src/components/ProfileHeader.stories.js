import ProfileHeader from "./ProfileHeader";
import ProfileModel from "@/models/ProfileModel";
import AccountModel from "@/models/AccountModel";

const profileAccount = new AccountModel("111-111-111-111", "test@test.com", "GenericUser", new Date());
const profile = new ProfileModel(profileAccount, 10, 10, 10);

export default {
  title: "Components/ProfileHeader",
  template: ProfileHeader
};

const Template = args => ({
  components: { ProfileHeader },
  setup: () => ({ args }),
  template: "<ProfileHeader v-bind='args'/>"
});

export const Default = Template.bind({});
Default.args = {
  profile
};
