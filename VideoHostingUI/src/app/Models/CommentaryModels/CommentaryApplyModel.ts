import { UUID } from "crypto";

export class CommentaryApplyModel {
  constructor(
    public userId: string,
    public content: string,
    public videoId: UUID
  ) {}
}
