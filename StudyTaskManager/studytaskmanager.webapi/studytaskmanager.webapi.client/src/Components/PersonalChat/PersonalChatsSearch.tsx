import React, { useState } from "react";
import { Me } from "../../TypesFromTheServer/Me";
import { User } from "../../TypesFromTheServer/User";
import axios from "axios";
import PersonalChatsSearchResultElement from "./PersonalChatsSearchResultElement";

interface PersonalChatsSearchProps {
    me: Me;
}

const PersonalChatsSearch: React.FC<PersonalChatsSearchProps> = ({ me }) => {
    const [searchUserName, setSearchUserName] = useState<string>("");
    const [resultUsers, setResultUsers] = useState<User[]>([]);

    const handleSearch = async () => {
        try {
            if (!searchUserName.trim()) {
                setResultUsers([]);
                return;
            }

            const response = await axios.get<User[]>(
                `https://localhost:7241/api/Users/Search`,
                {
                    withCredentials: true,
                    params: { userName: searchUserName }
                }
            );

            setResultUsers(response.data);
        } catch (error) {
            console.error("Error fetching users:", error);
            setResultUsers([]);
        }
    };

    return (
        <div style={{ width: '300px' }}>
            <div style={{ display: 'flex', marginBottom: '10px' }}>
                <input
                    style={{ flex: 1, padding: '8px', marginRight: '5px' }}
                    placeholder="Имя пользователя"
                    value={searchUserName}
                    onChange={(e) => setSearchUserName(e.target.value)}
                    onKeyPress={(e) => e.key === 'Enter' && handleSearch()}
                />
                <button onClick={handleSearch}>Поиск</button>
            </div>

            {resultUsers.length > 0 && (
                <ul style={{
                    listStyle: 'none',
                    padding: 0,
                    border: '1px solid #ddd',
                    borderRadius: '4px'
                }}>
                    {resultUsers.map(user => (
                        <li key={user.userId} style={{ padding: '8px', borderBottom: '1px solid #eee' }}>
                            <PersonalChatsSearchResultElement me={me} user={user} />
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default PersonalChatsSearch;