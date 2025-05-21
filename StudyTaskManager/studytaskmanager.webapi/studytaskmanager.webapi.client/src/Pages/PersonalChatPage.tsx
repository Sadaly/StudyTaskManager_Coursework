import { useEffect, useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { PersonalMessage } from '../TypesFromTheServer/PersonalMessage';
import PersonalMessageElem from '../Components/PersonalChat/PersonalMessageElem';
import { Me } from '../TypesFromTheServer/Me';
import PersonalMessageSendElem from '../Components/PersonalChat/PersonalMessageSendElem';

function PersonalChatPage() {
    const [me, setMe] = useState<Me | null>(null);
    const { idPersonalChat } = useParams<{ idPersonalChat: string }>();
    const [messages, setMessages] = useState<PersonalMessage[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const intervalRef = useRef<number>(1000);
    const messagesEndRef = useRef<HTMLDivElement>(null);

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

    const fetchMessages = async () => {
        try {
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

    useEffect(() => {
        const initializeChat = async () => {
            try {
                setLoading(true);
                await loadCurrentUser();
                await fetchMessages();

                intervalRef.current = setInterval(fetchMessages, 1000);

            } catch (err) {
                setError('Failed to initialize chat');
                console.error('Error initializing chat:', err);
            }
        };

        if (idPersonalChat) {
            initializeChat();
        }

        return () => {
            if (intervalRef.current) {
                clearInterval(intervalRef.current);
            }
        };
    }, [idPersonalChat]);

    useEffect(() => {
        messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    }, [messages]);

    if (me == null || loading) {
        return <p className="loading-text">Загрузка сообщений...</p>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div style={{
            maxWidth: '1200px',
            margin: '0 0 0 20px', // Прижимаем к левому краю с небольшим отступом
            padding: '20px',
            height: '100%',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <h2 style={{ marginBottom: '20px' }}>Personal Chat: {idPersonalChat}</h2>

            <div style={{
                flex: 1,
                overflowY: 'auto',
                marginBottom: '20px',
                padding: '15px'
            }}>
                {messages.length === 0 ? (
                    <p>Здесь пока нет сообщений</p>
                ) : (
                    messages.map((message) => (
                        <PersonalMessageElem
                            key={message.messageId}
                            message={message}
                            senderMy={message.senderId == me.userId}
                        />
                    ))
                )}
                <div ref={messagesEndRef} />
            </div>


            <div style={{ marginTop: 'auto' }}>
                <PersonalMessageSendElem
                    me={me}
                    personalChatId={idPersonalChat!}
                    onMessageSent={fetchMessages}
                />
            </div>
        </div>
    );
}

export default PersonalChatPage;