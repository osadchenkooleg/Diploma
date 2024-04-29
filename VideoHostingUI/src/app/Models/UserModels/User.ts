export class User {
  constructor(
    public id: string,
    public name: string,
    public email: string,
    public surname: string,
    public faculty: string,
    public group: string,
    public admin: boolean,
    public doSubscribed: boolean,
    public subscribers: boolean,
    public photoPath: string,
    public dateOfCreation: Date,
    public country: string,
    public sex: boolean,
    public subscriptions: number,
  ) 
  {}
}
