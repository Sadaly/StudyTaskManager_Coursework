import React, { useState, KeyboardEvent } from 'react';
import { Me } from '../../TypesFromTheServer/Me';
import axios from 'axios';

interface PersonalMessageSendElemProps {
    personalChatId: string;
    me: Me;
    onMessageSent?: () => void;
}

const PersonalMessageSendElem: React.FC<PersonalMessageSendElemProps> = ({
    me,
    personalChatId,
    onMessageSent
}) => {
    const [sendContent, setSendContent] = useState<string>("");
    const [error, setError] = useState<string | null>(null);

    const handleKeyDown = (e: KeyboardEvent<HTMLTextAreaElement>) => {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            handleSend();
        }
    };

    const handleSend = async () => {
        if (sendContent.trim()) {
            try {
                await axios.post(
                    'https://localhost:7241/api/PersonalMessage',
                    {
                        senderId: me.userId,
                        personalChatId: personalChatId,
                        content: sendContent
                    },
                    {
                        withCredentials: true,
                        headers: {
                            'Content-Type': 'application/json',
                            'accept': '*/*'
                        }
                    }
                );
                setSendContent("");
                setError(null);
                if (onMessageSent) {
                    onMessageSent();
                }
            } catch (err) {
                setError('Failed to send message');
                console.error('Error sending message:', err);
            }
        }
    };

    return (
        <div style={{ width: '100%' }}>
            <textarea
                style={{
                    width: '100%',
                    boxSizing: 'border-box',
                    padding: '8px',
                    minHeight: '40px',
                    resize: 'vertical',
                    fontFamily: 'inherit',
                    fontSize: 'inherit',
                }}
                placeholder="Сообщение"
                value={sendContent}
                onChange={(e) => setSendContent(e.target.value)}
                onKeyDown={handleKeyDown}
                rows={1}
            />
            {error && <p>{error}</p>}
        </div>
    );
};

export default PersonalMessageSendElem;