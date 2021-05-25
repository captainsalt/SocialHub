import AccountModel from "./AccountModel";

export default class ProfileModel {
  constructor(account: AccountModel, followers: number, following: number, totalPosts: number) {
    this.account = account;
    this.followers = followers;
    this.following = following;
    this.totalPosts = totalPosts;
  }

    account
    followers
    following
    totalPosts
}
