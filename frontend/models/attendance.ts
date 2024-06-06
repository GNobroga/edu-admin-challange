import { Subject } from "./subject";
import { User } from "./user"

export interface Attendance {
 id: number;
 student: Omit<User, 'type'>;
 date: string;
 present: boolean;
 subject: Subject;
}
