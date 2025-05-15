import React, { useEffect, useState } from 'react';
import axios from 'axios';

interface Group {
    id: string;
    title: { value: string };
    description?: { value: string };
    defaultRoleId: string;
}

const GroupsPage: React.FC = () => {
    const [groups, setGroups] = useState<Group[]>([]);
    const [searchTitle, setSearchTitle] = useState('');
    const [loading, setLoading] = useState(false);

    const fetchGroups = async () => {
        setLoading(true);
        try {
            const response = await axios.get('/api/Group/Take', {
                params: {
                    startIndex: 0,
                    count: 100,
                    'filter.Title': searchTitle || undefined,
                },
            });

            if (response.status === 200) {
                setGroups(response.data);
            }
        } catch (error) {
            console.error('Ошибка при загрузке групп:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchGroups();
    }, []);

    const handleSearch = () => {
        fetchGroups();
    };

    const handleCreateGroup = () => {
        // Здесь переход или логика создания новой группы
        alert('Форма создания группы пока не реализована');
    };

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h1 className="text-2xl font-bold mb-4">Поиск групп</h1>

            <div className="flex gap-2 mb-4">
                <input
                    type="text"
                    placeholder="Введите название группы"
                    value={searchTitle}
                    onChange={(e) => setSearchTitle(e.target.value)}
                    className="border px-4 py-2 rounded w-full"
                />
                <button
                    onClick={handleSearch}
                    className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                >
                    Поиск
                </button>
                <button
                    onClick={handleCreateGroup}
                    className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                >
                    Создать группу
                </button>
            </div>

            {loading ? (
                <p>Загрузка...</p>
            ) : (
                <ul className="space-y-4">
                    {groups.length === 0 && <p>Группы не найдены.</p>}
                    {groups.map((group) => (
                        <li
                            key={group.id}
                            className="border p-4 rounded shadow hover:shadow-lg transition"
                        >
                            <h2 className="text-xl font-semibold">{group.title.value}</h2>
                            {group.description && (
                                <p className="text-gray-600">{group.description.value}</p>
                            )}
                            <p className="text-sm text-gray-400 mt-2">
                                Default Role ID: {group.defaultRoleId}
                            </p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default GroupsPage;
