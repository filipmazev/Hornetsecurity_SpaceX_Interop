export class Error {
    baseCode: string;
    message: string;

    constructor(
        baseCode: 
        string, message: string) {
        this.baseCode = baseCode;
        this.message = message;
    }
}