import { UUID } from 'crypto';

export class Video {
  constructor(
    public id: UUID,
    public userId: string,
    public name: string,
    public dayOfCreation: Date,
    public views: number,
    public videoPath: string,
    public photoPath: string,
    public likes: number,
    public dislikes: number,
    public description: string,
    public liked: boolean,
    public disliked: boolean,
  ) {}
}
