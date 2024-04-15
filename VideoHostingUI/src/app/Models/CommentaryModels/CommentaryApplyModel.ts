import { UUID } from "crypto";

export class CommentaryApplyModel {
  public userId: string | undefined;
  public content: string | undefined;
  public videoId: UUID | undefined;
}
