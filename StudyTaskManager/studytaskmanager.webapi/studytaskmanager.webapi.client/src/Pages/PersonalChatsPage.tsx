import React, { useEffect, useState, useRef } from "react";
import { Outlet, Link } from "react-router-dom";
import axios from "axios";
import { Me } from "../TypesFromTheServer/Me";
import { PersonalChat } from "../TypesFromTheServer/PersonalChat";

const PersonalChatsPage = () => {
    const [me, setMe] = useState<Me | null>(null);
    const [chats, setChats] = useState<PersonalChat[]>([]);


    // Загрузка информации о текущем пользователе
    const loadCurrentUser = async () => {
        try {
            const response = await axios.get(
                "https://localhost:7241/api/users/me",
                { withCredentials: true, });
            setMe(response.data);
        } catch (error) {
            console.error("Ошибка при загрузке информации о пользователе", error);
        }
    };

    const loadPersonalChats = async () => {
        try {
            //if (me == null) {
            //    console.error("Ошибка при попытке получения данных");
            //    return;
            //}
            const response = await axios.get(
                `https://localhost:7241/api/PersonalChat/Chats/0196d1cd-4089-7999-9e90-6af880ae65ca`,
                {
                    withCredentials: true,
                });
            setChats(response.data)
        } catch (error) {
            console.error("Ошибка при загрузке персональных чатов", error);
        }
    };


    useEffect(() => {
        loadCurrentUser();
        loadPersonalChats();
    }, []);

    return (
        <div>
            <p>Персональные чаты</p>
            <div>
                {chats.map((chat) => (
                    <div key={chat.chatId}>
                        <p>{chat.chatId}</p>
                        <p>{chat.user1Id}</p>
                        <p>{chat.user2Id}</p>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default PersonalChatsPage;