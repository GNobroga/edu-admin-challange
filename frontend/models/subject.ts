import { Class } from "./class";
import { User } from "./user";

export interface Subject {
  name: string;
  classId: number;
  class?: Class;
  teacher?: User;
  teacherId: number;
}
