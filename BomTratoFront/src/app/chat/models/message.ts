import { MessageEnum } from "./enums/messageEnum";

export class Message {
    public userName: string;
    public content: string;
    public type: MessageEnum;

    constructor(userName: string, content: string, type: MessageEnum) {
        this.userName = userName;
        this.content = content;
        this.type = type;
    }
}