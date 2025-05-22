import React, { useState, useEffect } from 'react';
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
    const [isLoading, setIsLoading] = useState(false);
    const [dots, setDots] = useState(3);
    const navigate = useNavigate();

    // Анимация точек во время загрузки
    useEffect(() => {
        if (!isLoading) return;

        const interval = setInterval(() => {
            setDots(prev => prev > 1 ? prev - 1 : 3);
        }, 500);

        return () => clearInterval(interval);
    }, [isLoading]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsLoading(true);
        setErrorDetail('');
        setValidationErrors([]);

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
            navigate('/login');
        } catch (error: any) {
            const responseData = error.response?.data;
            setErrorDetail(responseData?.detail || 'Произошла ошибка при регистрации');

            if (Array.isArray(responseData?.errors)) {
                setValidationErrors(responseData.errors);
            }
            console.error('Registration failed', responseData);
        } finally {
            setIsLoading(false);
        }
    };

    const renderButtonText = () => {
        if (!isLoading) return 'Зарегистрироваться';

        const dotsText = '.'.repeat(dots);
        return `Загрузка${dotsText}`;
    };

    return (
        <div style={{
            marginLeft: "40vw",
            marginTop: "20vh",
            alignContent: 'center',
            padding: "2rem",
            boxSizing: "border-box"
        }}>
            <h1>Регистрация</h1>
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
                />
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Пароль"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <input
                    type="tel"
                    placeholder="Номер телефона (опционально)"
                    value={phoneNumber}
                    onChange={(e) => setPhoneNumber(e.target.value)}
                />
                <button
                    type="submit"
                    disabled={isLoading}
                    style={{
                        padding: "10px",
                        color: "white",
                        borderRadius: "5px",
                        cursor: isLoading ? "wait" : "pointer"
                    }}
                >
                    {renderButtonText()}
                </button>
            </form>

            {errorDetail && (
                <div style={{ color: 'red', marginTop: '10px' }}>
                    {errorDetail}
                </div>
            )}

            {validationErrors.length > 0 && (
                <ul style={{ color: 'red', marginTop: '10px' }}>
                    {validationErrors.map((err, idx) => (
                        <li key={idx}>{err.message}</li>
                    ))}
                </ul>
            )}

            <p style={{ marginTop: "20px" }}>
                Уже есть аккаунт? <Link to="/login">Вход</Link>
            </p>
        </div>
    );
}