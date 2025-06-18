export class UserModel {
  constructor(
    public id: number = 0,
    public username: string = "",
    public email: string = "",
    public age: number = 0,
    public firstName: string = "",
    public lastName: string = "",
    public gender: string = "",
    public image: string = "",
    public role: string = "",
    public address: Address = new Address()
  ) { }
}


export class Address {
  constructor(
    public address: string = "",
    public city: string = "",
    public state: string = "",
    public stateCode: string = "",
    public postalCode: string = "",
    public country: string = ""
  ) { }
}