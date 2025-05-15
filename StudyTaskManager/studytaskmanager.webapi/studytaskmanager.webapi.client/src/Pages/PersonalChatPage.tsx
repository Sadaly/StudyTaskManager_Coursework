import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { PersonalMessage } from '../TypesFromTheServer/PersonalMessage';
import MessageBubble from '../Components/PersonalChat/Message';


function PersonalChatPage() {
    const { id } = useParams<{ id: string }>();
    const [messages, setMessages] = useState<PersonalMessage[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchMessages = async () => {
            try {
                setLoading(true);
                const response = await axios.get<PersonalMessage[]>(
                    `https://localhost:7241/api/PersonalChat/${id}/Messages`
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

        if (id) {
            fetchMessages();
        }
    }, [id]);

    if (loading) {
        return <div>Loading messages...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h2>Personal Chat: {id}</h2>
            <div>
                {messages.length === 0 ? (
                    <p>No messages found</p>
                ) : (
                    <ul>
                        {messages.map((message) => (
                            <MessageBubble
                                key={message.messageId}
                                message={message}
                                //align={message.senderId === currentUserId ? 'right' : 'left'}
                                align={'left'}
                            />
                        ))}
                    </ul>
                )}
            </div>
        </div>
    );
}

export default PersonalChatPage;