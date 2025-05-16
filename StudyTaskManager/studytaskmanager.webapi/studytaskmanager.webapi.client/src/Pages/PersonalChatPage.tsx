import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { PersonalMessage } from '../TypesFromTheServer/PersonalMessage';
import PersonalMessageElem from '../Components/PersonalChat/PersonalMessageElem';
import { Me } from '../TypesFromTheServer/Me';


function PersonalChatPage() {
    const [me, setMe] = useState<Me | null>(null);
    const { idPersonalChat } = useParams<{ idPersonalChat: string }>();
    const [messages, setMessages] = useState<PersonalMessage[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

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

    useEffect(() => {
        const fetchMessages = async () => {
            try {
                setLoading(true);
                await loadCurrentUser();
                const response = await axios.get<PersonalMessage[]>(
                    `https://localhost:7241/api/PersonalChat/${idPersonalChat}/Messages`
                );
                setMessages(response.data);
                setError(null);
            } catch (err) {
                setError('Failed to fetch messages');
                console.error('Error fetching messages:', err);
            } finally {
                setLoading(false);
            }
        };

        if (idPersonalChat) {
            fetchMessages();
        }
    }, [idPersonalChat]);

    if (me == null || loading) {
        return <p className="loading-text">Загрузка сообщений...</p>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h2>Personal Chat: {idPersonalChat}</h2>
            <div>
                {messages.length === 0 ?
                    (<p>Здесь пока нет сообщений</p>)
                    : (
                        messages.map((message) => (<PersonalMessageElem
                            key={message.messageId}
                            message={message}
                            senderMy={message.senderId == me.userId}
                        />))
                    )
                }
            </div>
        </div>
    );
}

export default PersonalChatPage;