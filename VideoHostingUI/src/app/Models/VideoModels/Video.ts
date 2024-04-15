import { UUID } from 'crypto';

export class Video {
  public id: UUID | undefined;
  public userId: string | undefined;
  public name: string | undefined;
  public dayOfCreation: Date | undefined;
  public views: number | undefined;
  public videoPath: string | undefined;
  public photoPath: string | undefined;
  public likes: number | undefined;
  public dislikes: number | undefined;
  public description: string | undefined;
  public liked: boolean | undefined;
  public disliked: boolean | undefined;
}
