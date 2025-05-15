import axios from "axios";

export interface UserResponse {
    userId: string;
    username: string;
    email: string;
    isEmailVerifed: boolean;
    isPhoneNumberVerifed: boolean;
    phoneNumber: string | null;
    registrationDate: string;
    systemRoleId: string | null;
}

// Функция для получения пользователя по ID
const fetchUserById = async (userId: string): Promise<UserResponse | null> => {
    try {
        const response = await axios.get(
            `https://localhost:7241/api/Users/${userId}`,
            { withCredentials: true }
        );
        return response.data;
    } catch (error) {
        console.error(`Ошибка при загрузке пользователя с ID ${userId}`, error);
        return null;
    }
};