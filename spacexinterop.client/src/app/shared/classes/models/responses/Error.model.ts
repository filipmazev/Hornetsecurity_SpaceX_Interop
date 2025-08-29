export class Error {
    baseCode: string;
    messages: string[];

    constructor(
        baseCode: string, 
        messages: string[]) {
        this.baseCode = baseCode;
        this.messages = messages;
    }
}