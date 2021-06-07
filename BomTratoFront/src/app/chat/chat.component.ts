import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faArrowAltCircleRight, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { LocalStorageUtils } from '../utils/localstorage';
import { MessageEnum } from './models/enums/messageEnum';
import { Message } from './models/message';
import { ChatService } from './services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked {
  public messageEnumRef: typeof MessageEnum;
  public chatMessages: Message[];
  public sendMessageIcon: IconDefinition;
  public leaveChatIcon: IconDefinition;
  public liveChatOn: boolean;  
  localStorageUtils = new LocalStorageUtils();
  user =  this.localStorageUtils.getUser();


  @ViewChild('messagesContainer')
  private _messagesContainer: ElementRef;
  private _liveChatService: ChatService;
  private _router: Router;
  private _activatedRoute: ActivatedRoute;

  constructor(chatService: ChatService, router: Router, route: ActivatedRoute){
      this.chatMessages = [];
      this.messageEnumRef = MessageEnum;
      this._activatedRoute = route;
      this._liveChatService = chatService;
      this._router = router;
      this.liveChatOn = false;
      this.sendMessageIcon = faArrowAltCircleRight;
      this.leaveChatIcon = faSignOutAlt;
  }

  public ngAfterViewChecked(): void {
      if (this._messagesContainer && this.chatMessages.length > 0){
          this.scrollPageToBottom()
      }
  }

  public ngOnInit(): void {
      this._activatedRoute.queryParams.subscribe((params: Params) => {
          const userName = this.user.name;
          this._liveChatService.initializeNewUserConnectionAsync(userName)
              .then(() => {
                  this.liveChatOn = true;
              });
      });

      this._liveChatService.newMessageReceivedEvent.subscribe((newMessage: Message) => {
          this.chatMessages.push(newMessage);
      });
  }

  public sendNewMessage(messageInput: HTMLInputElement): void {
      const messageContent = messageInput.value;
      const currentUserName = this.user.name;
      const newMessage = new Message(currentUserName, messageContent, MessageEnum.CurrentUserMessage);
      this.chatMessages.push(newMessage);
      this._liveChatService.sendNewMessage(messageContent);
      messageInput.value = '';
  }

  public leaveChatAsync(): void {
      this._liveChatService.leaveChatAsync()
      .then(() => {
          this.liveChatOn = false;
          this._router.navigate(['']);
      });
  }

  private scrollPageToBottom(): void {
      this._messagesContainer.nativeElement.scrollTop =
      this._messagesContainer.nativeElement.scrollHeight;
  }

}
