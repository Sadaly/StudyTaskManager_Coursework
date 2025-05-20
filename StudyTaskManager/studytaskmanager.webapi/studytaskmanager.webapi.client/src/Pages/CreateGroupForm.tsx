import React, { useEffect, useState, FormEvent } from "react";
import axios from "axios";

interface Props {
    onSuccess?: () => void;
}

const CreateGroupForm = ({ onSuccess }: Props) => {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [creatorId, setCreatorId] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    // Получаем creatorId из /user/me
    useEffect(() => {
        axios.get("https://localhost:7241/api/Users/me", {
            withCredentials: true, })
            .then(res => setCreatorId(res.data.userId))
            .catch(() => setError("Не удалось получить данные пользователя"));
    }, []);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        setError(null);

        if (!creatorId) {
            setError("ID пользователя не получен");
            return;
        }

        setLoading(true);
        try {
            await axios.post("https://localhost:7241/api/group", {
                title,
                description,
                creatorId
            });
            setTitle("");
            setDescription("");
            onSuccess?.();
        } catch (err) {
            setError("Ошибка при создании группы");
        } finally {
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="p-4 border rounded space-y-4 max-w-md mx-auto">
            <h2 className="text-xl font-semibold">Создание новой группы</h2>

            <div>
                <label className="block text-sm font-medium mb-1">Название</label>
                <input
                    type="text"
                    className="w-full border p-2 rounded"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    required
                />
            </div>

            <div>
                <label className="block text-sm font-medium mb-1">Описание</label>
                <textarea
                    className="w-full border p-2 rounded"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    required
                />
            </div>

            {error && <p className="text-red-500 text-sm">{error}</p>}

            <button
                type="submit"
                className="bg-blue-600 text-white px-4 py-2 rounded disabled:opacity-50"
                disabled={loading || !creatorId}
            >
                {loading ? "Создание..." : "Создать группу"}
            </button>
        </form>
    );
};

export default CreateGroupForm;
