export class UserApplyModel {
  constructor(
    public email: string,
    public password: string,
    public passwordConfirm: string,
    public phoneNumber: string,
    public name: string,
    public surname: string,
    public faculty: string,
    public group: string,
    public sex: boolean
  )
  {}

}
