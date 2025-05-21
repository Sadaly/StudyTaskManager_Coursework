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
    name: string;
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
            await axios.post('https://localhost:7241/api/GroupTask', {
                groupId: id,
                deadline: newTask.deadline,
                statusId: newTask.statusId,
                headLine: newTask.headLine,
                description: newTask.description,
                responsibleUserId: null,
                parentTaskId: null,
            });

            setNewTask({
                headLine: '',
                description: '',
                deadline: new Date().toISOString(),
                statusId: statuses.length > 0 ? statuses[0].id : '',
            });

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

    if (loading) return <p className="text-center mt-10 text-lg">Загрузка...</p>;
    if (error) return <p className="text-center text-red-600">{error}</p>;
    if (!group) return <p className="text-center text-gray-500">Группа не найдена</p>;

    return (
        <div className="min-h-screen bg-gray-50 py-10 px-4 flex flex-col items-center">
            <div className="max-w-4xl w-full text-center mb-10">
                <h1 className="text-4xl font-bold text-blue-800">{group.title}</h1>
                <p className="text-lg text-gray-600 mt-2">
                    {group.description || 'Описание отсутствует'}
                </p>
            </div>

            {/* Создание задачи */}
            <div className="max-w-4xl w-full bg-white rounded-2xl shadow-lg p-8 mb-12 border border-blue-100">
                <h2 className="text-2xl font-semibold text-blue-700 mb-6 text-center">Создать новую задачу</h2>
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
                    <input
                        className="border border-gray-300 rounded-lg p-3 focus:outline-none focus:ring-2 focus:ring-blue-300"
                        placeholder="Заголовок"
                        value={newTask.headLine}
                        onChange={e => setNewTask({ ...newTask, headLine: e.target.value })}
                    />
                    <input
                        type="datetime-local"
                        className="border border-gray-300 rounded-lg p-3 focus:outline-none focus:ring-2 focus:ring-blue-300"
                        onChange={e => setNewTask({ ...newTask, deadline: new Date(e.target.value).toISOString() })}
                    />
                    <select
                        className="border border-gray-300 rounded-lg p-3 focus:outline-none focus:ring-2 focus:ring-blue-300"
                        value={newTask.statusId}
                        onChange={e => setNewTask({ ...newTask, statusId: e.target.value })}
                    >
                        {statuses.map(status => (
                            <option key={status.id} value={status.id}>{status.name}</option>
                        ))}
                    </select>
                </div>

                {/* Описание отдельно, на всю ширину */}
                <div className="w-full mt-6">
                    <textarea
                        className="w-full min-h-[120px] border border-gray-300 rounded-lg p-3 resize-y focus:outline-none focus:ring-2 focus:ring-blue-300"
                        placeholder="Описание"
                        value={newTask.description}
                        onChange={e => setNewTask({ ...newTask, description: e.target.value })}
                    />
                </div>

                <div className="flex justify-center mt-8">
                    <button
                        className="bg-blue-600 hover:bg-blue-700 text-white font-semibold px-8 py-3 rounded-lg transition"
                        onClick={createTask}
                    >
                        + Добавить задачу
                    </button>
                </div>
            </div>

            {/* Список задач */}
            <div className="max-w-6xl mx-auto px-6 py-10">
                <h2 className="text-4xl font-extrabold text-indigo-600 mb-12 text-center tracking-wide">
                    Задачи группы
                </h2>

                {tasks.length === 0 ? (
                    <p className="text-center text-gray-400 italic text-lg">Задачи пока отсутствуют</p>
                ) : (
                    <ul className="space-y-8">
                        {tasks.map(task => (
                            <li
                                key={task.id}
                                className="bg-gradient-to-r from-white via-indigo-50 to-white rounded-2xl shadow-lg p-8 flex flex-col md:flex-row md:justify-between md:items-center transition-transform hover:-translate-y-1 hover:shadow-2xl"
                            >
                                <div className="mb-6 md:mb-0 md:flex-1">
                                    <h3 className="text-2xl font-bold text-gray-900 mb-2">{task.headLine}</h3>
                                    <p className="text-gray-700 leading-relaxed mb-4 max-w-xl">{task.description}</p>
                                    <p className="text-sm text-gray-500">
                                        <strong>Дедлайн:</strong> {new Date(task.deadline).toLocaleString()}
                                    </p>
                                </div>

                                <div className="flex flex-col sm:flex-row items-center gap-5 md:gap-8">
                                    <select
                                        value={task.statusId}
                                        onChange={e => updateTaskStatus(task.id, e.target.value)}
                                        className="appearance-none bg-indigo-100 text-indigo-700 font-semibold px-5 py-3 rounded-xl shadow-sm cursor-pointer transition hover:bg-indigo-200 focus:outline-none focus:ring-4 focus:ring-indigo-300"
                                    >
                                        {statuses.map(status => (
                                            <option key={status.id} value={status.id}>{status.name}</option>
                                        ))}
                                    </select>

                                    <button
                                        onClick={() => deleteTask(task.id)}
                                        className="bg-red-500 text-white font-semibold px-6 py-3 rounded-xl shadow-md hover:bg-red-600 transition focus:outline-none focus:ring-4 focus:ring-red-300"
                                    >
                                        Удалить
                                    </button>
                                </div>
                            </li>
                        ))}
                    </ul>
                )}
            </div>


        </div>
    );

};

export default GroupDetailsPage;
