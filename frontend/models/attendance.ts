import { Subject } from "./subject";
import { User } from "./user"

export interface Attendance {
  student: User;
  date: string
  present: boolean
  subjectId: number
  subject: Subject;
  id: number
}
