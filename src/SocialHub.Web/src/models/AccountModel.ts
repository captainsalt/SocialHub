export default class AccountModel {
  constructor(id: string, username: string, email: string, creatonDate: Date) {
    this.id = id;
    this.username = username;
    this.email = email;
    this.creationDate = creatonDate;
  }

  id
  username
  email
  creationDate
}
