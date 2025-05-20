import React, { useEffect, useState, useCallback } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

interface Group {
    id: string;
    title: string;           // предполагаем, что title — строка
    description?: string;    // и описание — строка (если есть)
    defaultRoleId: string;
}

const GroupsPage: React.FC = () => {
    const [groups, setGroups] = useState<Group[]>([]);
    const [searchTitle, setSearchTitle] = useState('');
    const [loading, setLoading] = useState(false);
    const [startIndex, setStartIndex] = useState(0);
    const [count] = useState(10);
    const [hasMore, setHasMore] = useState(false);
    const navigate = useNavigate();

    const fetchGroups = useCallback(async () => {
        setLoading(true);
        try {
            const params: any = {
                startIndex,
                count,
            };
            if (searchTitle) {
                params['filter.Title'] = searchTitle;
            }

            const response = await axios.get('https://localhost:7241/api/Group/Take', { params });

            if (response.status === 200) {
                // Предполагаем, что API возвращает массив с такими полями
                // Подставляем в state группы полностью — если нужна постраничная загрузка с добавлением,
                // надо менять на setGroups(prev => [...prev, ...response.data])
                setGroups(response.data);

                setHasMore(response.data.length === count);
            }
        } catch (error) {
            console.error('Ошибка при загрузке групп:', error);
        } finally {
            setLoading(false);
        }
    }, [searchTitle, startIndex, count]);

    useEffect(() => {
        fetchGroups();
    }, [fetchGroups]);

    const handleCreateGroup = () => {
        navigate('/home/groups/create');
    };

    const handleNextPage = () => setStartIndex((prev) => prev + count);
    const handlePrevPage = () => setStartIndex((prev) => Math.max(prev - count, 0));

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
                    onClick={() => { setStartIndex(0); fetchGroups(); }}
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
                <>
                    <ul className="space-y-4">
                        {groups.length === 0 && <p>Группы не найдены.</p>}
                        {groups.map((group) => (
                            <li
                                key={group.id}
                                className="border p-4 rounded shadow cursor-pointer hover:bg-gray-100"
                                onClick={() => navigate(`/home/groups/${group.id}`)}
                            >
                                <h2 className="text-xl font-semibold">{group.title}</h2>
                                {group.description && (
                                    <p className="text-gray-600">{group.description}</p>
                                )}
                                <p className="text-sm text-gray-400 mt-2">
                                    Default Role ID: {group.id}
                                </p>
                            </li>
                        ))}

                    </ul>

                    <div className="flex justify-between mt-4">
                        <button
                            disabled={startIndex === 0}
                            onClick={handlePrevPage}
                            className="bg-gray-300 px-4 py-2 rounded disabled:opacity-50"
                        >
                            Назад
                        </button>
                        <button
                            disabled={!hasMore}
                            onClick={handleNextPage}
                            className="bg-gray-300 px-4 py-2 rounded disabled:opacity-50"
                        >
                            Вперёд
                        </button>
                    </div>
                </>
            )}
        </div>
    );
};

export default GroupsPage;
