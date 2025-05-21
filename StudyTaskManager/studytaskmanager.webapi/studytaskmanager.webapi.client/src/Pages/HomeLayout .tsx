import { Outlet, Link } from "react-router-dom";
import LogoutButton from "../Components/LogoutButton";

const HomeLayout = () => {
    return (
        <div style={{
            display: 'flex',
            margin: '0 0 0 20px', // Прижимаем к левому краю с небольшим отступом
            height: '100vh'
        }}>
            <nav style={{
                width: '200px',
                padding: '1rem',
                display: 'flex',
                flexDirection: 'column',
                height: '100%',
                borderRight: '1px solid #ccc'
            }}>
                <div>
                    <h2>Меню</h2>
                    <ul style={{
                        listStyle: 'none',
                        padding: 0
                    }}>
                        <li style={{ marginBottom: '10px' }}><Link to="/home">Главная</Link></li>
                        <li style={{ marginBottom: '10px' }}><Link to="/home/Chats">Чаты</Link></li>
                        <li style={{ marginBottom: '10px' }}><Link to="/home/groups">Группы</Link></li>
                    </ul>
                </div>
                <div style={{ marginTop: 'auto', marginBottom: '20px' }} >
                    <LogoutButton />
                </div>
            </nav>

            <div style={{
                flex: 1,
                padding: '0 0 0 1rem',
                overflowY: 'auto'
            }}>
                <Outlet />
            </div>
        </div>
    );
};

export default HomeLayout;