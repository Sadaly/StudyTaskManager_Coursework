import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import { User } from "../../TypesFromTheServer/User";
import { PersonalChat } from "../../TypesFromTheServer/PersonalChat";

interface ChatListItemProps {
    chat: PersonalChat;
    currentUserId: string;
}

const ChatListItem: React.FC<ChatListItemProps> = ({
    chat,
    currentUserId
}) => {
    const [otherUser, setOtherUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchOtherUser = async () => {
            try {
                // Определяем ID собеседника
                const otherUserId = chat.user1Id === currentUserId
                    ? chat.user2Id
                    : chat.user1Id;

                // Загружаем данные о пользователе
                const response = await axios.get(
                    `https://localhost:7241/api/Users/${otherUserId}`,
                    { withCredentials: true }
                );

                setOtherUser(response.data);
                setError(null);
            } catch (err) {
                console.error(`Ошибка при загрузке пользователя для чата ${chat.chatId}`, err);
                setError("Не удалось загрузить данные пользователя");
            } finally {
                setLoading(false);
            }
        };

        fetchOtherUser();
    }, [chat, currentUserId]);

    if (loading) {
        return <div className="chat-list-item">Загрузка чата ({chat.chatId})...</div>;
    }

    if (error) {
        return <div className="chat-list-item">{error}</div>;
    }

    return (
        <div className="chat-list-item">
            <p>
                <Link to={`/home/chats/${chat.chatId}`}>
                    {otherUser?.username || "Неизвестный пользователь"}
                </Link>
                <br />
                <span className="chat-id">(ID чата: {chat.chatId})</span>
            </p>
        </div>
    );
};

export default ChatListItem;