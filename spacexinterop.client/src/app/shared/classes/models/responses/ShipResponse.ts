import { EMPTY_STRING } from "../../../constants/common.constants";


export class ShipResponse {
    name: string = EMPTY_STRING;
    type?: string;
    image?: string;

    constructor(
        name: string,
        type: string | undefined,
        image: string | undefined
    ) {
        this.name = name;
        this.type = type;
        this.image = image;
    }
}
