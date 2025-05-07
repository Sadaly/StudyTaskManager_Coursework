import React, { useEffect, useState, useRef } from "react";
import axios from "axios";

interface User {
    id: string;
    username: string;
    email: string;
    registrationDate: string;
}

const HomePage: React.FC = () => {
    const [users, setUsers] = useState<User[]>([]);
    const [startIndex, setStartIndex] = useState(0);
    const [hasMore, setHasMore] = useState(true);
    const listRef = useRef<HTMLDivElement>(null);

    const loadUsers = async () => {
        try {
            const response = await axios.get("https://localhost:7241/api/Users/Take", {
                params: {
                    "StartIndex": startIndex,
                    "Count": 2,
                },
                withCredentials: true,
            });

            if (response.data.length === 0) {
                setHasMore(false);
                return;
            }

            setUsers(prev => [...prev, ...response.data]);
            setStartIndex(prev => prev + response.data.length);
        } catch (error) {
            console.error("Ошибка при загрузке пользователей", error);
        }
    };

    const handleScroll = () => {
        if (!listRef.current || !hasMore) return;

        const { scrollTop, scrollHeight, clientHeight } = listRef.current;
        if (scrollTop + clientHeight >= scrollHeight - 10) {
            loadUsers();
        }
    };

    useEffect(() => {
        loadUsers();
    }, []);

    return (
        <div
            ref={listRef}
            onScroll={handleScroll}
            style={{ height: "500px", overflowY: "auto", border: "1px solid gray", padding: "1rem" }}
        >
            {users.map((user) => (
                <div key={user.id}>
                    <h4>{user.username}</h4>
                    <p>Email: {user.email}</p>
                    <p>Дата регистрации: {new Date(user.registrationDate).toLocaleDateString()}</p>
                    <hr />
                </div>
            ))}
        </div>
    );
};

export default HomePage;
