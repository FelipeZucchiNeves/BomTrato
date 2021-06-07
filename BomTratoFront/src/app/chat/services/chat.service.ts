import * as signalR from '@microsoft/signalr';
import { EventEmitter } from '@angular/core';
import { Message } from '../models/message';
import { BaseService } from 'src/app/services/base.service';
import { MessageEnum } from '../models/enums/messageEnum';

export class ChatService extends BaseService {

    public onConnectionSuccessfully: EventEmitter<void>;
    public newMessageReceivedEvent: EventEmitter<Message>;
    public userEnteredEvent: EventEmitter<Message>;
    public userExitEvent: EventEmitter<Message>;

    private _hubConnection: signalR.HubConnection;
    private _currentUserName: string;

    constructor() {
        super()
        this.newMessageReceivedEvent = new EventEmitter<Message>();
        this.userEnteredEvent = new EventEmitter<Message>();
        this.userExitEvent = new EventEmitter<Message>();
        this.onConnectionSuccessfully = new EventEmitter();
        this._currentUserName = '';
    }

    public get CurrentUserName(): string {
        return this._currentUserName;
    }

    public initializeNewUserConnectionAsync(userName: string): Promise<void> {
        this._currentUserName = userName;
        this._hubConnection = new signalR.HubConnectionBuilder()
                                .withUrl(this.UrlServiceV1 + 'liveChatHub')
                                .build();

        this.assignNewMessageReceived();
        this.assignOnUserEnterChatAsync();
        this.assignOnUserExitChatAsync();

        return this._hubConnection.start().then(() => {
            this.onConnectionSuccessfully.emit();
            this._hubConnection.send('OnEnterChatAsync', this.CurrentUserName);
        });
    }

    public leaveChatAsync(): Promise<void> {
        return this._hubConnection.send('OnExitChatAsync', this.CurrentUserName)
                .then(() => {
                    this._hubConnection.stop();
                });
    }

    public sendNewMessage(message: string): void{
        this._hubConnection.send('OnNewMessageAsync', this.CurrentUserName, message);
    }

    public setUserName(name: string): void {
        localStorage.setItem('username', name);
    }

    private assignNewMessageReceived(): void {
        this._hubConnection.on('OnNewMessageAsync', (userName: string, messageContent: string) => {
            const newMessage = new Message(userName, messageContent, MessageEnum.OtherUser);
            this.newMessageReceivedEvent.emit(newMessage);
        });
    }

    private assignOnUserEnterChatAsync(): void {
        this._hubConnection.on('OnEnterChatAsync', (userName: string) => {
            const newMessage = new Message(userName, `${userName} ingressou no chat`, MessageEnum.ChatActions);
            this.newMessageReceivedEvent.emit(newMessage);
        });
    }

    private assignOnUserExitChatAsync(): void {
        this._hubConnection.on('OnExitChatAsync', (userName: string) => {
            const newMessage = new Message(userName, `${userName} saiu do chat`, MessageEnum.ChatActions);
            this.newMessageReceivedEvent.emit(newMessage);
        });
    }
}