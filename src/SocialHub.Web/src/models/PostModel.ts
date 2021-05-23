import AccountModel from "./AccountModel";

export default class PostModel {
  constructor(id: string, author: AccountModel, content: string, createdAt: Date) {
    this.id = id;
    this.author = author;
    this.content = content;
    this.createdAt = createdAt;
  }

  id
  author
  content
  createdAt
}
