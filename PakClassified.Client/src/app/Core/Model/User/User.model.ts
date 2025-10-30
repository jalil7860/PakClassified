import { Role } from "./Role.model";
export interface User {
    [x: string]: any;
    id: number;
    name?: string;
    email?: string;
    // apiKey?: string;
    // loginId?: string;
    password?: string;
    securityQuestion?: string;
    securityAnswer?: string;
    dateOfBirth: string;
    contactNumber?: string;
    image?: string;
    roleId: number;
    role?: Role;
}