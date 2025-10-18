import { Role } from "./Role.model";
export interface User {
    id: number;
    name?: string;
    email?: string;
    // apiKey?: string;
    // loginId?: string;
    password?: string;
    securityQuestion?: string;
    securityAnswer?: string;
    birthDate: string;
    contactNumber?: string;
    image?: string;
    roleId: number;
    role?: Role;
}