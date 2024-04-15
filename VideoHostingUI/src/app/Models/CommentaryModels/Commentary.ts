import { UUID } from "crypto";

export class Commentary {
  public id: UUID | undefined;
  public userId: string | undefined;
  public content: string | undefined;
  public dayOfCreation: Date | undefined;
  public videoId: UUID | undefined;
}
