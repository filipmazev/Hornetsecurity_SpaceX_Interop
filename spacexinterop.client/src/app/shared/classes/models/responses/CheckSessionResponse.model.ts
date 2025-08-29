export class CheckSessionResponse {
    userName: string;
    email: string;

    constructor(
        userName: string, 
        email: string) {
        this.userName = userName;
        this.email = email;
    }
}