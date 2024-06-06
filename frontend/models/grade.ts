import { Subject } from "./subject";
import { User } from "./user";

export interface Grade {
  id: number;
  value: number;
  student: Omit<User, 'type'>;
  subject: Subject & { teacher: Omit<User, 'type'> };
}
