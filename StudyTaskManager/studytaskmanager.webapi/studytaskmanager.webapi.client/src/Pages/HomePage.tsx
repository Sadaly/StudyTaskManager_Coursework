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
        <div className="min-h-screen bg-gray-50 p-6 flex flex-col items-center">
            {/* Блок с информацией о текущем пользователе */}
            {currentUser && (
                <section className="w-full max-w-4xl bg-white shadow-md rounded-lg p-6 mb-8 border border-gray-200">
                    <h2 className="text-2xl font-semibold mb-4 text-gray-800">Ваш профиль</h2>
                    <div className="text-gray-700 space-y-2">
                        <p><span className="font-medium">User ID:</span> {currentUser.userId ?? "--//--"}</p>
                        <p><span className="font-medium">Роль:</span> {currentUser.role}</p>
                    </div>
                </section>
            )}

            {/* Блок со списком пользователей */}
            <section
                ref={listRef}
                onScroll={handleScroll}
                className="w-full max-w-4xl bg-white shadow-md rounded-lg p-6 border border-gray-200 overflow-y-auto"
                style={{ height: '500px' }}
            >
                <h2 className="text-2xl font-semibold mb-6 text-gray-800">Список пользователей</h2>
                {users.length === 0 ? (
                    <p className="text-center text-gray-500">Пользователи не найдены</p>
                ) : (
                    <ul className="space-y-6">
                        {users.map((user) => (
                            <li key={user.userId} className="border-b border-gray-200 pb-4 last:border-b-0">
                                <h3 className="text-lg font-semibold text-gray-900">{user.username}</h3>
                                <p className="text-gray-600 leading-relaxed mt-1">
                                    <span className="font-medium">Email:</span> {user.email}<br />
                                    <span className="font-medium">Дата регистрации:</span> {new Date(user.registrationDate).toLocaleDateString()}<br />
                                    <span className="font-medium">ID:</span> {user.userId}
                                </p>
                            </li>
                        ))}
                    </ul>
                )}
            </section>
        </div>
    );

};

export default HomePage;