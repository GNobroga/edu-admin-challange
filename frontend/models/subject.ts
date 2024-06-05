import { Class } from "./class";
import { User } from "./user";

export interface Subject {
  id: number;
  name: string;
  class: Class;
  teacher: Omit<User, 'type'>;
}
