import React, { useEffect, useState } from "react";
import axios from "axios";
import { Me } from "../TypesFromTheServer/Me";
import { PersonalChat } from "../TypesFromTheServer/PersonalChat";
import Accordion from "../Components/Accordion";
import PersonalChatListItem from "../Components/PersonalChat/PersonalChatListItem";
import PersonalChatsSearch from "../Components/PersonalChat/PersonalChatsSearch";

const PersonalChatsPage = () => {
    const [load, setLoad] = useState<boolean>(false);
    const [me, setMe] = useState<Me | null>(null);
    const [chats, setChats] = useState<PersonalChat[]>([]);

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

    const loadPersonalChats = async (userId: string) => {
        try {
            const response = await axios.get(
                `https://localhost:7241/api/PersonalChat/Chats/${userId}`,
                { withCredentials: true }
            );
            setChats(response.data);
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

    if (me == null || load) {
        return (<p className="loading-text">Загрузка чатов...</p>);
    }

    return (
        <div>
            <Accordion title="Открыть новый чат">
                <PersonalChatsSearch me={me} />
            </Accordion>

            <p>Персональные чаты</p>
            {chats.map((chat) => (
                <PersonalChatListItem
                    key={chat.chatId}
                    chat={chat}
                    currentUserId={me.userId}
                />
            ))}
        </div>
    );
};

export default PersonalChatsPage;