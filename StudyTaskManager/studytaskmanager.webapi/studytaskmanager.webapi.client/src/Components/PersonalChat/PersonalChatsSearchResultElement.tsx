import React from "react";
import { User } from "../../TypesFromTheServer/User";
import { Me } from "../../TypesFromTheServer/Me";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";

interface PersonalChatsSearchResultElementProps {
    me: Me;
    user: User;
}

const PersonalChatsSearchResultElement: React.FC<PersonalChatsSearchResultElementProps> = ({ me, user }) => {
    const navigate = useNavigate();

    const handleCreateChatAndNavigate = async () => {
        try {
            const response = await axios.post(
                "https://localhost:7241/api/PersonalChat",
                {
                    user1: me.userId,
                    user2: user.userId
                }
            );

            if (response.status === 200) {
                const chatId = response.data.id; // Предполагаем, что сервер возвращает объект с id чата
                navigate(`/home/chats/${chatId}`);
            }
        } catch (error) {
            console.error("Error creating chat:", error);
            // Здесь можно добавить обработку ошибок, например, показать уведомление пользователю
        }
    };

    return (
        <div key={user.userId}>
            <button onClick={handleCreateChatAndNavigate} style={{ background: 'none', border: 'none', cursor: 'pointer' }}>
                {user.username}
            </button>
        </div>
    );
};

export default PersonalChatsSearchResultElement;