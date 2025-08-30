export enum ResultStatusEnum {
  //#region Basic
  Default = 'Default',
  Success = 'Success',
  Failure = 'Failure',
  NotFound = 'NotFound',
  ValidationFailed = 'ValidationFailed',
  Exception = 'Exception',
  InvalidType = 'InvalidType',
  //#endregion

  //#region Auth
  EmailAlreadyExists = 'EmailAlreadyExists',
  //#endregion
}
