import React, { useEffect, useState } from "react";
import { Outlet, Link } from "react-router-dom";
import axios from "axios";
import { Me } from "../TypesFromTheServer/Me";
import { PersonalChat } from "../TypesFromTheServer/PersonalChat";
import Accordion from "../Components/Accordion";
import { User } from "../TypesFromTheServer/User";

const PersonalChatsPage = () => {
    const [load, setLoad] = useState<Boolean>(false);
    const [me, setMe] = useState<Me | null>(null);
    const [chats, setChats] = useState<PersonalChat[]>([]);
    const [openDivFromCreateNew, setOpenDivFromCreateNew] = useState<Boolean>(false);
    const [serchUserName, setSerchUserName] = useState<string>();
    const [usersData, setUsersData] = useState<Record<string, User>>({});

    // Загрузка информации о текущем пользователе
    const loadCurrentUser = async () => {
        try {
            const response = await axios.get(
                "https://localhost:7241/api/users/me",
                { withCredentials: true }
            );
            setMe(response.data);
            return response.data;
        } catch (error) {
            console.error("Ошибка при загрузке информации о пользователе", error);
            return null;
        }
    };

    // Функция для получения пользователя по ID
    const fetchUserById = async (userId: string): Promise<User | null> => {
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

    const loadPersonalChats = async (userId: string) => {
        try {
            const response = await axios.get(
                `https://localhost:7241/api/PersonalChat/Chats/${userId}`,
                { withCredentials: true }
            );
            setChats(response.data);

            // Загружаем данные о пользователях для каждого чата
            const users: Record<string, User> = {};
            for (const chat of response.data) {
                // Определяем ID другого пользователя в чате
                const otherUserId = chat.user1Id === userId ? chat.user2Id : chat.user1Id;

                // Загружаем данные пользователя, если еще не загружены
                if (!users[otherUserId]) {
                    const userData = await fetchUserById(otherUserId);
                    if (userData) {
                        users[otherUserId] = userData;
                    }
                }
            }

            setUsersData(users);
        } catch (error) {
            console.error("Ошибка при загрузке персональных чатов", error);
        }
    };

    // Функция для получения имени пользователя в чате
    const getChatUserName = (chat: PersonalChat): string => {
        if (!me) return "Unknown User";

        // Определяем ID другого пользователя
        const otherUserId = chat.user1Id === me.userId ? chat.user2Id : chat.user1Id;

        // Возвращаем имя пользователя или "Unknown User", если данные не загружены
        return usersData[otherUserId]?.username || "Unknown User";
    };

    useEffect(() => {
        setLoad(true);
        const loadData = async () => {
            const userData = await loadCurrentUser();
            if (userData) {
                await loadPersonalChats(userData.userId);
            }
            setLoad(false);
        };

        loadData();
    }, []);

    return (
        <div>
            <Accordion title="Открыть новый чат">
                <input
                    type="search"
                    placeholder="search"
                    value={serchUserName}
                    onChange={(e) => setSerchUserName(e.target.value)}
                    required
                />
                <button>Search</button>
            </Accordion>

            <p>Персональные чаты {load ? "loading..." : "loaded"}</p>

            {chats.map((chat) => (
                <div key={chat.chatId}>
                    <p><Link to={`/home/chats/${chat.chatId}`}>
                        {getChatUserName(chat)}
                    </Link> <br />(Chat ID: {chat.chatId})    </p>
                </div>
            ))}
        </div>
    );
};

export default PersonalChatsPage;