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
    const [count] = useState(5);
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
        <div className="p-8 max-w-4xl mx-auto bg-white rounded-lg shadow-lg">
            <h1 className="text-3xl font-extrabold mb-6 text-gray-800 text-center">Поиск групп</h1>

            <div className="flex flex-col sm:flex-row gap-4 mb-6">
                <input
                    type="text"
                    placeholder="Введите название группы"
                    value={searchTitle}
                    onChange={(e) => setSearchTitle(e.target.value)}
                    className="flex-grow border border-gray-300 rounded-lg px-5 py-3 text-gray-700 placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-blue-400 transition"
                />
                <button
                    onClick={() => { setStartIndex(0); fetchGroups(); }}
                    className="bg-blue-600 hover:bg-blue-700 text-white font-semibold rounded-lg px-6 py-3 transition shadow-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                    Поиск
                </button>
                <button
                    onClick={handleCreateGroup}
                    className="bg-green-600 hover:bg-green-700 text-white font-semibold rounded-lg px-6 py-3 transition shadow-md focus:outline-none focus:ring-2 focus:ring-green-500"
                >
                    Создать группу
                </button>
            </div>

            {loading ? (
                <p className="text-center text-gray-500 text-lg">Загрузка...</p>
            ) : (
                <>
                    {groups.length === 0 ? (
                        <p className="text-center text-gray-500 text-lg">Группы не найдены.</p>
                    ) : (
                        <ul className="space-y-6">
                            {groups.map((group) => (
                                <li
                                    key={group.id}
                                    className="border border-gray-200 rounded-lg p-5 shadow-sm cursor-pointer hover:shadow-lg hover:bg-blue-50 transition"
                                    onClick={() => navigate(`/home/groups/${group.id}`)}
                                >
                                    <h2 className="text-2xl font-semibold text-gray-800">{group.title}</h2>
                                    {group.description && (
                                        <p className="text-gray-600 mt-2">{group.description}</p>
                                    )}
                                    <p className="text-sm text-gray-400 mt-3 italic">ID группы: {group.id}</p>
                                </li>
                            ))}
                        </ul>
                    )}

                    <div className="flex justify-between mt-8">
                        <button
                            disabled={startIndex === 0}
                            onClick={handlePrevPage}
                            className={`px-6 py-3 rounded-lg font-medium transition 
              ${startIndex === 0
                                    ? 'bg-gray-300 cursor-not-allowed text-gray-500'
                                    : 'bg-gray-200 hover:bg-gray-300 text-gray-700'}`}
                        >
                            Назад
                        </button>
                        <button
                            disabled={!hasMore}
                            onClick={handleNextPage}
                            className={`px-6 py-3 rounded-lg font-medium transition 
              ${!hasMore
                                    ? 'bg-gray-300 cursor-not-allowed text-gray-500'
                                    : 'bg-gray-200 hover:bg-gray-300 text-gray-700'}`}
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
