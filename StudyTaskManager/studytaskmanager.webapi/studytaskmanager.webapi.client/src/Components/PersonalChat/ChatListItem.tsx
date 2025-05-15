import React from "react";
import { Link } from "react-router-dom";
import { PersonalChat } from "../../TypesFromTheServer/PersonalChat";
import { User } from "../../TypesFromTheServer/User";

interface ChatListItemProps {
    chat: PersonalChat;
    currentUserId: string;
    usersData: Record<string, User>;
}

const ChatListItem: React.FC<ChatListItemProps> = ({
    chat,
    currentUserId,
    usersData
}) => {
    const getChatUserName = (): string => {
        if (!currentUserId) return "Unknown User";

        const otherUserId = chat.user1Id === currentUserId
            ? chat.user2Id
            : chat.user1Id;

        return usersData[otherUserId]?.username || "Unknown User";
    };

    return (
        <div className="chat-list-item">
            <p>
                <Link to={`/home/chats/${chat.chatId}`}>
                    {getChatUserName()}
                </Link>
                <br />
                <span className="chat-id">(Chat ID: {chat.chatId})</span>
            </p>
        </div>
    );
};

export default ChatListItem;