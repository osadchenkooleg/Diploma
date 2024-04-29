import { CommentaryApplyModel } from './../../../Models/CommentaryModels/CommentaryApplyModel';
import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Commentary } from '../../../Models/CommentaryModels/Commentary';
import { Subject, map, tap } from 'rxjs';
import { AuthService } from '../../../Services/auth.service';
import { CommentaryService } from '../../../Services/commentary.service';
import { UUID } from 'crypto';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material/material.module';
import { FormsModule } from '@angular/forms';
import { CommentItemComponent } from "../comment-item/comment-item.component";

@Component({
    selector: 'app-comment-list',
    standalone: true,
    templateUrl: './comment-list.component.html',
    styleUrl: './comment-list.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        FormsModule,
        CommentItemComponent
    ]
})
export class CommentListComponent implements OnInit { 
  @Input() videoId: UUID | null = null;
  @Input() comments: Commentary[] = [];

  isEditing:boolean = false;

  onItemDeleted = new Subject<any>();
  onItemAdded = new Subject<any>();

  commentText:string| null = null;

  isAdmin:boolean = false;
  userId:string | null = null;

  @ViewChild("commentinput") inputField: ElementRef | undefined;
  
  constructor(
    private as: AuthService,
    private cs: CommentaryService
  ) { }

  ngOnInit() {
    this.isAdmin = this.as.isAdmin();
    this.userId = this.as.getUserId();
  }

  isAuthor(comment:Commentary):boolean{
    if(comment.userId === this.userId)
      return true;
    return false;
  }
  postComment(){
    if(this.commentText!== null && this.userId !== null && this.videoId !== null){
      this.cs.createCommentary(new CommentaryApplyModel(this.userId, this.commentText, this.videoId))
        .pipe(
          map(data =>{
            this.comments?.push(data);
          }),
          tap(item => this.onItemAdded.next(item))
        ).subscribe(res =>{
          this.commentText = null;
          this.inputField?.nativeElement.blur();
        });
    }
  }

  deleteComment(comment:Commentary){
    this.cs.deleteCommentary(comment.id)
      .pipe(
        map(data => {
          this.comments = this.removeById(this.comments, data.id);
          return this.comments;
        }),
        tap(items => {
          items.forEach(element => {
            console.log("items: " + element);
          });
          console.log("items: " + items);
          this.onItemDeleted.next(items);
          this.onItemAdded.next(items);
        }),
      ).subscribe();
  }
  updateElement(fromItems : Commentary[], id: UUID, newElement:Commentary) {
    const index1  = fromItems.findIndex((element) => {
      return element.id === id;
    });
    if (index1 >= 0 ) {
      fromItems[index1] = newElement;
    }
    return fromItems;
  }
  removeById(fromItems : Commentary[], id: UUID) {
    console.log("before: " + fromItems);
    const index1  = fromItems.findIndex((element) => {
      return element.id === id;
    });
    console.log(index1);
    if (index1 >= 0 ) {
      fromItems.splice(index1,1);
    }
    console.log("after: " + fromItems);
    return fromItems;
  }
}
