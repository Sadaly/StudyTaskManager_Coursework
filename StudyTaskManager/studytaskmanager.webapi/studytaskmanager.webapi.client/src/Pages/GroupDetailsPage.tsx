import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import axios from 'axios';

interface Group {
    id: string;
    title: string;
    description?: string;
}

interface Task {
    id: string;
    headLine: string;
    description: string;
    deadline: string;
    statusId: string;
}

interface TaskStatus {
    id: string;
    title: string;
}

const GroupDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [group, setGroup] = useState<Group | null>(null);
    const [tasks, setTasks] = useState<Task[]>([]);
    const [statuses, setStatuses] = useState<TaskStatus[]>([]);
    const [newTask, setNewTask] = useState({
        headLine: '',
        description: '',
        deadline: new Date().toISOString(),
        statusId: '',
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!id) return;

        const fetchData = async () => {
            try {
                setLoading(true);

                const [groupRes, tasksRes, statusesRes] = await Promise.all([
                    axios.get(`https://localhost:7241/api/Group/${id}`),
                    axios.get(`https://localhost:7241/api/GroupTask/Group/${id}`),
                    axios.get('https://localhost:7241/api/Common/GroupTaskStatus'),
                ]);

                setGroup(groupRes.data);
                setTasks(tasksRes.data);
                setStatuses(statusesRes.data);

                if (statusesRes.data.length > 0) {
                    setNewTask(prev => ({ ...prev, statusId: statusesRes.data[0].id }));
                }
            } catch (err) {
                setError('Ошибка при загрузке данных');
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [id]);

    const fetchTasks = async () => {
        if (!id) return;
        try {
            const response = await axios.get(`https://localhost:7241/api/GroupTask/Group/${id}`);
            setTasks(response.data);
        } catch (err) {
            console.error('Ошибка при загрузке задач');
        }
    };

    const createTask = async () => {
        try {
            const response = await axios.post('https://localhost:7241/api/GroupTask', {
                groupId: id,
                deadline: newTask.deadline,
                statusId: newTask.statusId,
                headLine: newTask.headLine,
                description: newTask.description,
                responsibleUserId: null,
                parentTaskId: null,
            });

            console.log('Задача создана:', response.data);
            await fetchTasks();
        } catch (error) {
            console.error('Ошибка при создании задачи:', error);
        }
    };

    const deleteTask = async (taskId: string) => {
        try {
            await axios.delete(`https://localhost:7241/api/GroupTask/${taskId}`);
            await fetchTasks();
        } catch (error) {
            console.error('Ошибка при удалении задачи:', error);
        }
    };

    const updateTaskStatus = async (taskId: string, newStatusId: string) => {
        try {
            const task = tasks.find(t => t.id === taskId);
            if (!task) return;

            await axios.put(`https://localhost:7241/api/GroupTask/${taskId}`, {
                ...task,
                statusId: newStatusId,
            });

            await fetchTasks();
        } catch (error) {
            console.error('Ошибка при обновлении статуса:', error);
        }
    };

    if (loading) return <p>Загрузка...</p>;
    if (error) return <p>{error}</p>;
    if (!group) return <p>Группа не найдена</p>;

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h1 className="text-3xl font-bold mb-4">{group.title}</h1>
            {group.description ? (
                <p className="text-lg text-gray-700 mb-6">{group.description}</p>
            ) : (
                <p className="text-gray-500 italic mb-6">Описание отсутствует</p>
            )}

            <h2 className="text-2xl font-semibold mb-2">Создать задачу</h2>
            <div className="mb-4 space-y-2">
                <input
                    className="w-full border px-3 py-2 rounded"
                    placeholder="Заголовок"
                    value={newTask.headLine}
                    onChange={e => setNewTask({ ...newTask, headLine: e.target.value })}
                />
                <textarea
                    className="w-full border px-3 py-2 rounded"
                    placeholder="Описание"
                    value={newTask.description}
                    onChange={e => setNewTask({ ...newTask, description: e.target.value })}
                />
                <input
                    className="w-full border px-3 py-2 rounded"
                    type="datetime-local"
                    onChange={e => setNewTask({ ...newTask, deadline: new Date(e.target.value).toISOString() })}
                />
                <select
                    className="w-full border px-3 py-2 rounded"
                    value={newTask.statusId}
                    onChange={e => setNewTask({ ...newTask, statusId: e.target.value })}
                >
                    {statuses.map(status => (
                        <option key={status.id} value={status.id}>{status.title}</option>
                    ))}
                </select>
                <button
                    className="bg-blue-600 text-white px-4 py-2 rounded"
                    onClick={createTask}
                >
                    Создать задачу
                </button>
            </div>

            <h2 className="text-2xl font-semibold mb-2">Задачи</h2>
            <ul className="space-y-4">
                {tasks.map(task => (
                    <li key={task.id} className="border p-4 rounded">
                        <h3 className="text-xl font-medium">{task.headLine}</h3>
                        <p className="text-gray-700">{task.description}</p>
                        <p className="text-sm text-gray-500">Дедлайн: {new Date(task.deadline).toLocaleString()}</p>
                        <div className="mt-2 flex gap-2 items-center">
                            <select
                                value={task.statusId}
                                onChange={e => updateTaskStatus(task.id, e.target.value)}
                                className="border rounded px-2 py-1"
                            >
                                {statuses.map(status => (
                                    <option key={status.id} value={status.id}>{status.title}</option>
                                ))}
                            </select>
                            <button
                                onClick={() => deleteTask(task.id)}
                                className="bg-red-500 text-white px-3 py-1 rounded"
                            >
                                Удалить
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default GroupDetailsPage;
