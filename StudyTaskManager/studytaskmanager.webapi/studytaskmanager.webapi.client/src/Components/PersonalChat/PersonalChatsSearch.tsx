import React, { useState } from "react";
import { Me } from "../../TypesFromTheServer/Me";
import { User } from "../../TypesFromTheServer/User";
import axios from "axios";
import PersonalChatsSearchResultElement from "./PersonalChatsSearchResultElement";

interface PersonalChatsSearchProps {
    me: Me
}

const PersonalChatsSearch: React.FC<PersonalChatsSearchProps> = ({ me }) => {
    const [searchUserName, setSearchUserName] = useState<string>("");
    const [resultUsers, setResultUsers] = useState<User[]>([]);

    const handleSearch = async () => {
        try {
            if (!searchUserName.trim()) {
                // Не делаем запрос если строка пустая
                return;
            }

            const response = await axios.get<User[]>(
                `https://localhost:7241/api/Users/Search`,
                {
                    withCredentials: true,
                    params: {
                        userName: searchUserName
                    }
                }
            );

            setResultUsers(response.data);
        } catch (error) {
            console.error("Error fetching users:", error);
            // Можно добавить обработку ошибки, например:
            // setError("Failed to search users");
        }
    };

    return (
        <>
            <input
                placeholder="Имя пользователя"
                value={searchUserName}
                onChange={(e) => setSearchUserName(e.target.value)}
                required
            />
            <button onClick={handleSearch}>Search</button>

            {/* Можно добавить отображение результатов */}
            {resultUsers.length > 0 && (
                <div>
                    <ul>
                        {resultUsers.map(user => (
                            <PersonalChatsSearchResultElement key={user.userId} me={me} user={user} />
                        ))}
                    </ul>
                </div>
            )}
        </>
    );
};

export default PersonalChatsSearch;