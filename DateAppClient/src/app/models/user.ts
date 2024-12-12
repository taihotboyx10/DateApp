export interface User{
    userName: string,
    token: string,
}

export class RegisterUserDTO{
    UserName: string = '';
    Pwd: string = '';
    DoB: Date = new Date();
    CreateDate: Date = new Date();
    Gender: string | null = null;
    Introductions: string | null = null;
    Interest: string | null = null;
    LookingFor: string | null = null;
    City: string | null = null;
    Country: string | null = null;
}

export interface AllUser {
    id: number,
    userName?: string;
    age: number;
    lastActive: Date;
    gender?: string;
    introductions?: string;
    address?: string;
  }