import { UUID } from "crypto";

export class Video {
  id: UUID | undefined;
  UserId: string | undefined;
  Name: string | undefined;
  DayOfCreation: Date | undefined;
  Views: number | undefined;
  VideoPath: string | undefined;
  PhotoPath: string | undefined;
  Likes: number | undefined;
  Dislikes: number | undefined;
  Description: string | undefined;
  Liked: boolean | undefined;
  Disliked: boolean | undefined;
}
