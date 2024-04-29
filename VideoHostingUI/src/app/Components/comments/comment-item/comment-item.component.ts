import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Commentary } from '../../../Models/CommentaryModels/Commentary';
import { AuthService } from '../../../Services/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';
import { UserItemComponent } from "../../user/user-item/user-item.component";

@Component({
    selector: 'app-comment-item',
    standalone: true,
    templateUrl: './comment-item.component.html',
    styleUrl: './comment-item.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        FormsModule,
        UserItemComponent
    ]
})
export class CommentItemComponent implements OnInit {
  commentText:string = "";

  isAuthor:boolean = false;
  isAdmin:boolean = false;
  
  @Output() commentToDelete = new EventEmitter<Commentary>();  
  @Input() comment: Commentary | undefined;
  constructor(
    private as:AuthService
  ) { }
  
  
  ngOnInit() {
    this.isAuthor = this.getAuthor();
    this.isAdmin = this.as.isAdmin();
  }
  deleteComment(){
    this.commentToDelete.emit(this.comment)
  }
  getAuthor():boolean{
    if(this.comment?.userId === this.as.getUserId())
      return true;
    return false;
  }
 }
