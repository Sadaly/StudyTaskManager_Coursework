import React, { useEffect, useState } from "react";
import { Outlet, Link } from "react-router-dom";
import axios from "axios";
import { Me } from "../TypesFromTheServer/Me";
import { PersonalChat } from "../TypesFromTheServer/PersonalChat";
import Accordion from "../Components/Accordion";
import { User } from "../TypesFromTheServer/User";
import ChatListItem from "../Components/PersonalChat/ChatListItem";

const PersonalChatsPage = () => {
    const [load, setLoad] = useState<Boolean>(false);
    const [me, setMe] = useState<Me | null>(null);
    const [chats, setChats] = useState<PersonalChat[]>([]);
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
        me == null || load ?
            <p>Загрузка...</p> :
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

                <p>Персональные чаты</p>

                {chats.map((chat) => (
                    <ChatListItem
                        key={chat.chatId}
                        chat={chat}
                        currentUserId={me.userId}
                        usersData={usersData}
                    />
                ))
                }
            </div>
    );
};

export default PersonalChatsPage;