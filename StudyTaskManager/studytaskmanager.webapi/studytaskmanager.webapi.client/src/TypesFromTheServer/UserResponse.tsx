export interface UserResponse {
    userId: string;
    username: string;
    email: string;
    isEmailVerifed: boolean;
    isPhoneNumberVerifed: boolean;
    phoneNumber: string;
    registrationDate: string;
    systemRoleId: string | null;
}