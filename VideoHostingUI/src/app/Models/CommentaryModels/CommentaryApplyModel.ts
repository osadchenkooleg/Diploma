import { UUID } from "crypto";

export class CommentaryApplyModel {
  userId: string | undefined;
  content: string | undefined;
  videoId: UUID | undefined;
}
