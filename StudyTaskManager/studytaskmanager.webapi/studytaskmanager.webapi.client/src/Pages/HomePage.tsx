import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import { Me } from "../TypesFromTheServer/Me";


interface User {
    userId: string;
    username: string;
    email: string;
    registrationDate: string;
}


const HomePage: React.FC = () => {
    const [users, setUsers] = useState<User[]>([]);
    const [currentUser, setCurrentUser] = useState<Me | null>(null);
    const [startIndex, setStartIndex] = useState(0);
    const [hasMore, setHasMore] = useState(true);
    const listRef = useRef<HTMLDivElement>(null);

    // Загрузка информации о текущем пользователе
    const loadCurrentUser = async () => {
        try {
            const response = await axios.get("https://localhost:7241/api/Users/me", {
                withCredentials: true,
            });
            setCurrentUser(response.data);
        } catch (error) {
            console.error("Ошибка при загрузке информации о пользователе", error);
        }
    };

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
        loadCurrentUser(); // Загружаем информацию о текущем пользователе
        loadUsers();
    }, []);

    return (
        <div>
            {/* Блок с информацией о текущем пользователе */}
            {currentUser && (
                <div style={{ marginLeft: "150px", padding: "1rem", borderBottom: "1px solid gray" }}>
                    <h2>Ваш профиль</h2>
                    <p>userId: {currentUser.userId ? currentUser.userId : "--//--"}</p>
                    <p>role: {currentUser.role}</p>
                </div>
            )}

            {/* Блок со списком пользователей */}
            <div
                ref={listRef}
                onScroll={handleScroll}
                style={{
                    marginLeft: "150px",
                    width: "100%",
                    height: "500px",
                    overflowY: "auto",
                    border: "1px solid gray",
                    padding: "1rem"
                }}
            >
                <h2>Список пользователей</h2>
                {users.map((user) => (
                    <div key={user.userId}>
                        <h4>{user.username}</h4>
                        <p>
                            Email: {user.email}<br />
                            Дата регистрации: {new Date(user.registrationDate).toLocaleDateString()}<br />
                            Id: {user.userId}
                        </p>
                        <hr />
                    </div>
                ))}
            </div>
        </div>
    );
};

export default HomePage;