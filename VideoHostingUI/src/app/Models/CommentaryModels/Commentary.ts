import { UUID } from "crypto";

export class Commentary {
  constructor(
    public id: UUID,
    public userId: string,
    public content: string,
    public dayOfCreation: Date,
    public videoId: UUID
  ) {}
}
