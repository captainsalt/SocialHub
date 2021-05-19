import AccountModel from "./AccountModel";

export default class PostModel {
  constructor(author: AccountModel, content: string) {
    this.author = author;
    this.content = content;
  }

  author
  content
}
