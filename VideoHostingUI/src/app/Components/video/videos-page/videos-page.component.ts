import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-videos-page',
  standalone: true,
  imports: [
    CommonModule,
  ],
  templateUrl: './videos-page.component.html',
  styleUrl: './videos-page.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VideosPageComponent { }
