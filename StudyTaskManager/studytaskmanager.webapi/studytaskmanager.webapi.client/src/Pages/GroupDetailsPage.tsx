import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import axios from 'axios';

interface Group {
    id: string;
    title: string;
    description?: string;
}

const GroupDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [group, setGroup] = useState<Group | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!id) return;

        const fetchGroup = async () => {
            try {
                setLoading(true);
                const response = await axios.get(`https://localhost:7241/api/Group/${id}`);
                if (response.status === 200) {
                    setGroup(response.data);
                } else {
                    setError('Группа не найдена');
                }
            } catch (err) {
                setError('Ошибка при загрузке данных группы');
            } finally {
                setLoading(false);
            }
        };

        fetchGroup();
    }, [id]);

    if (loading) return <p>Загрузка...</p>;
    if (error) return <p>{error}</p>;
    if (!group) return <p>Группа не найдена</p>;

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h1 className="text-3xl font-bold mb-4">{group.title}</h1>
            {group.description ? (
                <p className="text-lg text-gray-700">{group.description}</p>
            ) : (
                <p className="text-gray-500 italic">Описание отсутствует</p>
            )}
        </div>
    );
};

export default GroupDetailsPage;
