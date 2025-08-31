export class PayloadResponse {
    name?: string;
    type?: string;
    reused: boolean;
    customers: string[] = [];
    manufacturers: string[] = [];

    constructor(
        name: string | undefined, 
        type: string | undefined, 
        reused: boolean, 
        customers: string[] = [], 
        manufacturers: string[] = []) {
        this.name = name;
        this.type = type;
        this.reused = reused;
        this.customers = customers;
        this.manufacturers = manufacturers;
    }
}