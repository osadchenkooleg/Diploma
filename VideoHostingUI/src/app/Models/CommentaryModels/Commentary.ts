import { UUID } from "crypto";

export class Commentary {
  id: UUID | undefined;
  userId: string | undefined;
  content: string | undefined;
  dayOfCreation: Date | undefined;
  videoId: UUID | undefined;
}
