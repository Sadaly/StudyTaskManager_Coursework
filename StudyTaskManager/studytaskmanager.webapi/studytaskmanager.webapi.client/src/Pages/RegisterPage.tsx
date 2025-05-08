import React, { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';

interface ValidationError {
    code: string;
    message: string;
}

export default function RegisterPage() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [errorDetail, setErrorDetail] = useState('');
    const [validationErrors, setValidationErrors] = useState<ValidationError[]>([]);
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const command = {
            username,
            email,
            password,
            phoneNumber: phoneNumber || null,
            systemRoleId: null
        };

        try {
            const response = await axios.post('https://localhost:7241/api/Users/Registration', command, {
                headers: {
                    'Content-Type': 'application/json',
                },
                withCredentials: true,
            });

            console.log('User registered successfully:', response.data.message);
            setErrorDetail('');
            setValidationErrors([]);
            navigate('/login'); // Перенаправляем на страницу входа после успешной регистрации
        } catch (error: any) {
            const responseData = error.response?.data;

            // Устанавливаем основное сообщение
            setErrorDetail(responseData?.detail || 'Произошла ошибка при регистрации');

            // Устанавливаем ошибки валидации, если есть
            if (Array.isArray(responseData?.errors)) {
                setValidationErrors(responseData.errors);
            } else {
                setValidationErrors([]);
            }

            console.error('Registration failed', responseData);
        }
    };

    return (
        <div style={{
            //backgroundColor: "#2d0a0a",
            marginLeft: "150px",
            width: "100%",
            minHeight: "100vh",
            padding: "2rem",
            boxSizing: "border-box",
            color: "white"
        }}>
            <h2>Регистрация</h2>
            <form
                onSubmit={handleSubmit}
                style={{ display: "flex", flexDirection: "column", width: "300px", gap: "10px" }}
            >
                <input
                    type="text"
                    placeholder="Имя"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    required
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <input
                    type="password"
                    placeholder="Пароль"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <input
                    type="tel"
                    placeholder="Номер телефона (необязательно)"
                    value={phoneNumber}
                    onChange={(e) => setPhoneNumber(e.target.value)}
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <button
                    type="submit"
                    style={{
                        padding: "10px",
                        backgroundColor: "#f44336",
                        color: "white",
                        border: "none",
                        borderRadius: "5px",
                        cursor: "pointer"
                    }}
                >
                    Зарегистрироваться
                </button>
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

            <p style={{ marginTop: "20px" }}>
                Уже есть аккаунт? <Link to="/login" style={{ color: "#f44336" }}>Вход</Link>
            </p>
        </div>
    );
}