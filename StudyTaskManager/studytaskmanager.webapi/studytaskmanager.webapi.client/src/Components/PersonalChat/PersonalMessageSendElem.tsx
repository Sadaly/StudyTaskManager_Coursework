import React, { useState } from 'react';

interface PersonalMessageSendElemProps {
}

const PersonalMessageSendElem: React.FC<PersonalMessageSendElemProps> = () => {
    const [sendContent, setSendContent] = useState<string>("");

    const handleSearch = () => {
    };

    return (
        <div>
            <input
                placeholder="Сообщение"
                value={sendContent}
                onChange={(e) => setSendContent(e.target.value)}
                required
            />
        </div>
    );
};

export default PersonalMessageSendElem;