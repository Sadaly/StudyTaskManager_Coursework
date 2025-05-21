import React, { useState } from "react";
import { User } from "../../TypesFromTheServer/User";
import { Me } from "../../TypesFromTheServer/Me";
import { useNavigate } from "react-router-dom";
import axios from "axios";

interface PersonalChatsSearchResultElementProps {
    me: Me;
    user: User;
}

const PersonalChatsSearchResultElement: React.FC<PersonalChatsSearchResultElementProps> = ({ me, user }) => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);

    const handleCreateChatAndNavigate = async () => {
        try {
            setIsLoading(true);
            const response = await axios.post(
                "https://localhost:7241/api/PersonalChat",
                {
                    user1: me.userId,
                    user2: user.userId
                },
                { withCredentials: true }
            );

            if (response.status === 200) {
                navigate(`/home/chats/${response.data.id}`);
            }
        } catch (error) {
            console.error("Error creating chat:", error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <span>{user.username}</span>
            <button
                onClick={handleCreateChatAndNavigate}
                disabled={isLoading}
                style={{
                    padding: '4px 8px',
                    backgroundColor: '#eee',
                    border: '1px solid #ccc',
                    borderRadius: '3px',
                    cursor: 'pointer'
                }}
            >
                {isLoading ? '...' : 'Чат'}
            </button>
        </div>
    );
};

export default PersonalChatsSearchResultElement;