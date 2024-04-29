export class UserUpdateModel {
  constructor(
    public name:string,
    public surname: string,
    public faculty: string,
    public group: string,
    public sex: boolean
  ) {}
}
