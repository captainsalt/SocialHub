import AccountModel from "./AccountModel";

export default class PostModel {
  constructor(id: string, author: AccountModel, content: string, createdAt: Date, isLiked: boolean, isShared: boolean) {
    this.id = id;
    this.account = author;
    this.content = content;
    this.createdAt = createdAt;
    this.isLiked = isLiked;
    this.isShared = isShared;
  }

  id
  account
  content
  createdAt
  isLiked
  isShared
}
