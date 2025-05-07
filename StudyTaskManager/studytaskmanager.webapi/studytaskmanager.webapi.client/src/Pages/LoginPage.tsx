import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

interface ValidationError {
    code: string;
    message: string;
}

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorDetail, setErrorDetail] = useState('');
    const [validationErrors, setValidationErrors] = useState<ValidationError[]>([]);
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const command = { email, password };

        try {
            const response = await axios.post('https://localhost:7241/api/Users/Login', command, {
                headers: {
                    'Content-Type': 'application/json',
                },
                withCredentials: true,
            });

            console.log('User logged in successfully:', response.data.message);
            setErrorDetail('');
            setValidationErrors([]);
            navigate('/home');
        } catch (error: any) {
            const responseData = error.response?.data;

            // Устанавливаем основное сообщение
            setErrorDetail(responseData?.detail || 'Произошла ошибка входа');

            // Устанавливаем ошибки валидации, если есть
            if (Array.isArray(responseData?.errors)) {
                setValidationErrors(responseData.errors);
            } else {
                setValidationErrors([]);
            }

            console.error('Login failed', responseData);
        }
    };

    return (
        <div>
            <h2>Вход</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email:</label><br />
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Пароль:</label><br />
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Войти</button>
            </form>

            {/* Общая ошибка */}
            {errorDetail && (
                <div style={{ color: 'red', marginTop: '10px' }}>
                    {errorDetail}
                </div>
            )}

            {/* Ошибки валидации */}
            {validationErrors.length > 0 && (
                <ul style={{ color: 'red', marginTop: '10px' }}>
                    {validationErrors.map((err, idx) => (
                        <li key={idx}>{err.message}</li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default LoginPage;
