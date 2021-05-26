import AccountModel from "./AccountModel";

export default class ProfileModel {
  constructor(account: AccountModel, followers: number, following: number, totalPosts: number, isFollowing: boolean) {
    this.account = account;
    this.followers = followers;
    this.following = following;
    this.totalPosts = totalPosts;
    this.isFollowing = isFollowing;
  }

  account
  followers
  following
  totalPosts
  isFollowing
}
